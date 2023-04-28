using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace BaseCode.Data
{
    public static class Helper
    {
        public static IQueryable<T> OrderByPropertyName<T>(this IQueryable<T> query, string attribute, string direction)
        {
            return ApplyOrdering(query, attribute, direction, "OrderBy");
        }

        public static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string attribute, string direction)
        {
            return ApplyOrdering(query, attribute, direction, "ThenBy");
        }

        private static IQueryable<T> ApplyOrdering<T>(IQueryable<T> query, string attribute, string direction, string orderMethodName)
        {
            try
            {
                if (direction == Constants.SortDirection.Descending) orderMethodName += Constants.SortDirection.Descending;

                var t = typeof(T);

                var param = Expression.Parameter(t);
                var property = t.GetProperty(attribute);

                if (property != null)
                    return query.Provider.CreateQuery<T>(
                        Expression.Call(
                            typeof(Queryable),
                            orderMethodName,
                            new[] { t, property.PropertyType },
                            query.Expression,
                            Expression.Quote(
                                Expression.Lambda(
                                    Expression.Property(param, property),
                                    param))
                        ));
                else
                    return query;
            }
            catch (Exception) // Probably invalid input, you can catch specifics if you want
            {
                return query; // Return unsorted query
            }
        }

        public static HttpResponseMessage ComposeResponse(HttpStatusCode statusCode, object responseData)
        {
            var jsonResponse = JsonConvert.SerializeObject(responseData);
            var resp = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };

            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return resp;
        }

        public static Dictionary<string, string[]> GetModelStateErrors(ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
        }
        public static void GetErrors(Exception ex, out HttpStatusCode responseCode, out object responseData)
        {
            var message = Constants.Exception.ErrorProcessing;

            responseCode = HttpStatusCode.BadRequest;
            responseData = message;
        }

        #region Upload File
        public static ExpandoObject GetFormData(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                dynamic formObject = new ExpandoObject();
                var count = result.FormData.Keys.Count;
                for (int i = 0; i < count; i++)
                {
                    var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(i).FirstOrDefault() ?? String.Empty);
                    var type = unescapedFormData.GetType();
                    if (!String.IsNullOrEmpty(unescapedFormData))
                    {
                        switch (i)
                        {
                            case 0:
                                formObject.Id = (type == typeof(String)) ? unescapedFormData : JsonConvert.DeserializeObject(unescapedFormData);
                                break;

                            case 1:
                                formObject.ModuleName = (type == typeof(String)) ? unescapedFormData : JsonConvert.DeserializeObject(unescapedFormData);
                                break;

                            default:
                                break;
                        }
                    }
                }
                return formObject;
            }
            return null;
        }
        public static string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = fileData.Headers.ContentDisposition.FileName;
            return JsonConvert.DeserializeObject(fileName).ToString();
        }
        public static void CreateDirectory(string pathToCreate)
        {
            if (!DoesPathExists(pathToCreate))
            {
                System.IO.Directory.CreateDirectory(pathToCreate);
            }
        }

        public static bool DoesPathExists(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Download File
        /// <summary>
        /// Function used to send an email notification
        /// </summary>
        /// <param name="fileName">Name of the file being downloaded</param>
        /// <param name="filePath">Folder path where file is located</param>
        /// <returns>An HttpResponseMessage object containing the file/message </returns>
        public static HttpResponseMessage DownloadFile(string fileName, string filePath)
        {
            HttpResponseMessage result = new HttpResponseMessage();

            try
            {
                // Check if filename is null or empty
                // If empty, return error message. Else, proceed
                if (!string.IsNullOrEmpty(fileName))
                {
                    var path = filePath + fileName;

                    // Check if the directory and file exists
                    if (Directory.Exists(filePath) && File.Exists(path))
                    {
                        // Convert the file into bytes
                        byte[] bytes = File.ReadAllBytes(path);

                        // Check if the file has actual content
                        // If empty, return erro message. Else proceed
                        if (bytes.Length == 0)
                        {
                            result = ComposeResponse(HttpStatusCode.NotFound, "File is empty");
                        }
                        else
                        {
                            // Create the actual response along with the file byte data
                            result.Content = new ByteArrayContent(bytes);
                            result.Content.Headers.Add("x-filename", fileName);
                            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                            result.Content.Headers.ContentDisposition.FileName = fileName;
                            result.StatusCode = HttpStatusCode.OK;
                        }
                    }
                    else
                    {
                        result = ComposeResponse(HttpStatusCode.NotFound, "File cannot be found");
                    }
                }
                else
                {
                    result = ComposeResponse(HttpStatusCode.NotFound, "File cannot be found");
                }
            }
            catch
            {
                result = ComposeResponse(HttpStatusCode.InternalServerError, Constants.Common.BadRequest);
            }

            return result;
        }
        #endregion

        #region Send Email
        /// <summary>
        /// Function used to send an email notification
        /// </summary>
        /// <param name="toList">List of email address to send email</param>
        /// <param name="ccList">List of email address to send email as CC</param>
        /// <param name="subject">Holds the subject of the email</param>
        /// <param name="body">Holds the content of the email</param>
        /// <returns>Boolean value if email was sent</returns>
        public static bool SendEmailNotification(List<string> toList, List<string> ccList, List<string> bccList, string subject, string body)
        {
            bool isSent = false;
            try
            {     
                // Check if there are any recipients in the first place
                // Else cancel the sending of the email
                if (toList?.Count > 0)
                {
                    // Initialize an new SMTP client
                    var smtpClient = new SmtpClient(Constants.SMTP.SMTPClient)
                    {
                        Credentials = new NetworkCredential(Constants.SMTP.EmailAddress, Constants.SMTP.EmailPassword),
                        EnableSsl = true,
                        Port = 587
                    };
                    
                    // Initialize an empty mail message
                    var mail = new MailMessage();
                    
                    // Create the recipient TO list
                    foreach (var email in toList)
                    {
                        mail.To.Add(email);
                    }

                    // Create the recipient CC list
                    if (ccList?.Count > 0)
                    {
                        foreach (var email in ccList)
                        {
                            mail.CC.Add(email);
                        }
                    }

                    // Create the recipient BCC list
                    if (bccList?.Count > 0)
                    {
                        foreach (var email in bccList)
                        {
                            mail.Bcc.Add(email);
                        }
                    }

                    // Setup the rest of the mail information
                    mail.From = new MailAddress(Constants.SMTP.EmailAddress, Constants.SMTP.EmailName);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;                    
                    mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, Constants.SMTP.Format));
                    
                    // Send the email
                    smtpClient.Send(mail);
                    isSent = true;
                }
            }
            catch (Exception ex) { }

            return isSent;
        }
        #endregion

        #region Export to Excel
        public static HttpResponseMessage ExportToExcel(string content, string fileName)
        {
            HttpResponseMessage result = new HttpResponseMessage();
            try
            {
                if (!string.IsNullOrEmpty(content))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(content.ToString());

                    result.Content = new ByteArrayContent(bytes.ToArray());
                    result.Content.Headers.Add("x-filename", fileName);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msexcel");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = fileName;
                    result.StatusCode = HttpStatusCode.OK;
                    return result;
                }
                else
                {
                    result = ComposeResponse(HttpStatusCode.NotFound, "No records found");
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = ComposeResponse(HttpStatusCode.InternalServerError, Constants.Common.BadRequest);
            }

            return result;
        }
        #endregion

        #region Export to PDF
        public static HttpResponseMessage ExportToPdf(string content, string path, bool flag)
        {
            Byte[] Data = null;

            // do some additional cleansing to handle some scenarios that are out of control with the html data
            content = content.Replace("<br>", "<br />");

            // convert html to pdf
            try
            {
                // create a stream that we can write to, in this case a MemoryStream
                using (var stream = new MemoryStream())
                {
                    // create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
                    using (var document = new Document(PageSize.LETTER, 35f, 35f, 25f, 25f))
                    {
                        if (flag)
                        {
                            document.SetPageSize(PageSize.A4.Rotate());
                        }
                        // create a writer that's bound to our PDF abstraction and our stream
                        using (var writer = PdfWriter.GetInstance(document, stream))
                        {
                            // open the document for writing
                            document.Open();
                            if ((!String.IsNullOrEmpty(path)) && (path != null))
                            {
                                Image jpg = Image.GetInstance(path);
                                //Resize image depend upon your need
                                jpg.ScaleToFit(140f, 120f);
                                document.Add(jpg);
                            }

                            // read html data to StringReader
                            using (var html = new StringReader(content))
                            {
                                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, html);
                            }

                            // close document
                            document.Close();
                        }
                    }

                    // get bytes from stream
                    Data = stream.ToArray();

                    HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                    httpResponseMessage.Content = new ByteArrayContent(Data.ToArray());
                    httpResponseMessage.Content.Headers.Add("x-filename", "initial.pdf");
                    httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    httpResponseMessage.Content.Headers.ContentDisposition.FileName = "initial.pdf";
                    httpResponseMessage.StatusCode = HttpStatusCode.OK;
                    return httpResponseMessage;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return result;
            }
        }
        #endregion
    }
}
