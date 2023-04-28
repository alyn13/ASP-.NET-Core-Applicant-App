using Basecode.Data.ViewModels.Common;
using BaseCode.API.Utilities;
using BaseCode.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using MobileJO.Data.ViewModels.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MobileJO.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    public class AttachmentAPIController : ApiController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public AttachmentAPIController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///     Downloads a file from the server and returns it to the frontend as byte array data.
        /// </summary>
        /// <returns>Represents an HTTP response that includes the status code, data and message</returns>
        [HttpGet]
        [ActionName("downloadAttachment")]
        public HttpResponseMessage DownloadAttachment([FromQuery] AttachmentViewModel model)
        {
            var response = new HttpResponseMessage();

            try
            {
                var rootFolderPath = _hostingEnvironment.ContentRootPath;
                var attachmentPath = Configuration.Config.GetSection(Constants.Attachment.AttachmentPath).Value;
                var filePath = string.Format(Constants.Attachment.FullFilePath, rootFolderPath, attachmentPath);
                var fileName = model.FileName;
                return Helper.DownloadFile(fileName, filePath);
            }
            catch (Exception ex)
            {
                var responseCode = new HttpStatusCode();
                var responseData = new object();

                Helper.GetErrors(ex, out responseCode, out responseData);
                response = Helper.ComposeResponse(responseCode, responseData);
            }

            return response;
        }

        /// <summary>
        ///     Uploads a file to the server
        /// </summary>
        /// <returns>Represents an HTTP response that includes the status code, data and message</returns>
        [HttpPost]
        [ActionName("upload")]
        public async Task<HttpResponseMessage> UploadAttachment()
        {
            var response = new HttpResponseMessage();

            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var filePath = string.Empty;
                var relativeFilePath = string.Empty;
                var fileToDelete = string.Empty;
                var files = new List<FileViewModel>();

                string rootFolder = _hostingEnvironment.ContentRootPath;
                var provider = new MultipartFormDataStreamProvider(rootFolder);
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                // Get uploaded files
                foreach (var file in provider.FileData)
                {
                    string fileName = Helper.GetDeserializedFileName(file);
                    filePath = rootFolder + "\\Uploads\\";
                    relativeFilePath = rootFolder + "\\Uploads\\";

                    files.Add(new FileViewModel
                    {
                        FilePath = relativeFilePath,
                        NonRelativePath = filePath,
                        LocalFileName = file.LocalFileName,
                        FileName = fileName
                    });
                }

                // Move files to directories
                foreach (var file in files)
                {
                    File.Move(file.LocalFileName, file.FileAbsolutePath);
                }

                return Request.CreateResponse(HttpStatusCode.OK, "File Upload Success!");
            }
            catch (Exception ex)
            {
                var responseCode = new HttpStatusCode();
                var responseData = new object();

                Helper.GetErrors(ex, out responseCode, out responseData);
                response = Helper.ComposeResponse(responseCode, responseData);
            }

            return response;
        }

        /// <summary>
        ///     Sample function for generating and downloading an excel file
        /// </summary>
        /// <returns>Represents an HTTP response that includes the status code, data and message</returns>
        [HttpGet]
        [ActionName("downloadExcel")]
        public HttpResponseMessage DownloadExcel()
        {
            var response = new HttpResponseMessage();

            try
            {
                var excel = new StringBuilder();
                excel.Append(Constants.Reports.ExcelTable);
                response = Helper.ExportToExcel(excel.ToString(), string.Format("{0}.xls", "Sample"));
            }
            catch (Exception ex)
            {
                var responseCode = new HttpStatusCode();
                var responseData = new object();

                Helper.GetErrors(ex, out responseCode, out responseData);
                response = Helper.ComposeResponse(responseCode, responseData);
            }

            return response;
        }

        /// <summary>
        ///     Sample function for generating and downloading a pdf file
        /// </summary>
        /// <returns>Represents an HTTP response that includes the status code, data and message</returns>
        [HttpGet]
        [ActionName("downloadPDF")]
        public HttpResponseMessage DownloadPDF()
        {
            var response = new HttpResponseMessage();

            try
            {
                string content = File.ReadAllText(Path.GetFullPath("Uploads/RequestForPayment.html"));
                var pdf = new StringBuilder();
                pdf.Append(Constants.Reports.PDFContent);
                response = Helper.ExportToPdf(pdf.ToString(), string.Empty, false);
            }
            catch (Exception ex)
            {
                var responseCode = new HttpStatusCode();
                var responseData = new object();

                Helper.GetErrors(ex, out responseCode, out responseData);
                response = Helper.ComposeResponse(responseCode, responseData);
            }

            return response;
        }

        /// <summary>
        ///     Sample function for generating and downloading a pdf file
        /// </summary>
        /// <returns>Represents an HTTP response that includes the status code, data and message</returns>
        [HttpGet]
        [ActionName("sendEmail")]
        public HttpResponseMessage SendEmail()
        {
            var response = new HttpResponseMessage();

            try
            {
                var toList = new List<string>();
                var ccList = new List<string>();
                var bccList = new List<string>();

                toList.Add("");
                ccList.Add("");
                string subject = "Sample Basecode Email";
                string body = "<body><p color='black' bgcolor='#D4D4D4'>Hi,</p><p></br></br></br>" + 
                    "This is a sample email.</p></br></br></br><p>Thank you,</p></br></br></br><p>Mobile JO</p></body>";
                var send = Helper.SendEmailNotification(toList, ccList, bccList, subject, body);
                return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Common.EmailSuccess);
            }
            catch (Exception ex)
            {
                var responseCode = new HttpStatusCode();
                var responseData = new object();

                Helper.GetErrors(ex, out responseCode, out responseData);
                response = Helper.ComposeResponse(responseCode, responseData);
            }

            return response;
        }
    }
}