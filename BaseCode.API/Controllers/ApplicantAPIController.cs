using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using Constants = BaseCode.Data.Constants;

using BaseCode.Domain.Services;
using System.Net.Mail;

namespace BaseCode.API.Controllers
{
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = Constants.Common.Bearer, Roles = Constants.Roles.ASI_HR)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ApplicantAPIController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly IMapper _mapper;

        public ApplicantAPIController(IApplicantService applicantService, IMapper mapper)
        {
            _applicantService = applicantService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Applicant record.
        /// </summary>
        /// <param name="id">ID of the Applicant record</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous] 
        [ActionName("getApplicant")]
        public HttpResponseMessage GetApplicant(int id)
        {
            var applicant = _applicantService.Find(id);
            return applicant != null ? Helper.ComposeResponse(HttpStatusCode.OK, applicant) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Applicant.ApplicantDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Applicant records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Applicant records</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("list")]
        public HttpResponseMessage GetApplicantList([FromQuery] ApplicantSearchViewModel searchModel)
        {
            var responseData = _applicantService.FindApplicants(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        /// <summary>
        ///     This function adds a Applicant record.
        /// </summary>
        /// <param name="applicantModel">Contains Applicant properties</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ActionName("add")]
        public HttpResponseMessage PostApplicant(ApplicantViewModel applicantModel)
        {
            /*try { if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState)); }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                var innerExceptionStackTrace = ex.InnerException?.StackTrace;

                Console.WriteLine("An error occurred while updating the entries.");
                Console.WriteLine("Inner exception message: " + innerExceptionMessage);
                Console.WriteLine("Inner exception stack trace: " + innerExceptionStackTrace);
            }*/

             if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            
            try
            {
                var applicant = _mapper.Map<Applicant>(applicantModel);
                var validationErrors = new ApplicantHandler(_applicantService).CanAdd(applicant);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
               
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    string fromMail = "groupfour.alliance@gmail.com";
                    string password = "jtdslucomfdnmvoa";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(fromMail);
                    message.Subject = "[Alliance Software, Inc] Acknowledgment of Job Application";
                    message.To.Add(new MailAddress(applicant.Email));
                    message.Body = $"<html><body><p>Dear {applicant.FirstName},</p>"+
                        $"<p>I hope this email finds you well. I am writing to confirm that we have received your job application for the position of {applicant.JobApplied} at Alliance Software, Inc. </p>"+
                        "<p>We appreciate your interest in joining our team and would like to assure you that your application is currently under review. </p><p>Our HR team is currently reviewing applications, "+
                        "and we appreciate your interest in joining our organization. We understand that waiting can be challenging, but please be assured that your qualifications and experience will "+
                        "be carefully assessed in relation to the requirements of the role. </p><p>Just keep posted as we will notify you of the update of the status of your application. Should any further "+
                        "information be required, we will contact you using the details provided in your application. </p> <p> </p> <p>Thank you for your patience and consideration.</p>" +
                        "<p></p><p>Best regards,</p> <p>HR Recruitment Associate<br>Alliance Software, Inc.</p></body></html>";

                    message.IsBodyHtml = true;

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, password),
                        EnableSsl = true
                    };

                    smtpClient.Send(message);


                    _applicantService.Create(applicant);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function updates an Applicant record.
        /// </summary>
        /// <param name="applicantModel">Contains Applicant properties</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("edit")]
        [AllowAnonymous]
        public HttpResponseMessage PutApplicant(ApplicantViewModel applicantModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var applicant = _mapper.Map<Applicant>(applicantModel);
                var validationErrors = new ApplicantHandler(_applicantService).CanUpdate(applicant);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    //var claimsIdentity = User.Identity as ClaimsIdentity;
                    /* if (claimsIdentity != null)
                     {
                         student.ModifiedBy = claimsIdentity.Name;
                         student.ModifiedDate = DateTime.Now;
                     }*/

                    _applicantService.Update(applicant);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function deletes an Applicant record.
        /// </summary>
        /// <param name="id">ID of the Applicant record</param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous] //change later
        [ActionName("delete")]
        public HttpResponseMessage DeleteApplicant(int id)
        {
            try
            {
                var validationErrors = new ApplicantHandler(_applicantService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _applicantService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessDelete);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }
    }
}
