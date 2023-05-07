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


namespace BaseCode.API.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.Common.Bearer, Roles = Constants.Roles.Admin)]
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

            var temp = 0;
            try { if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState)); }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message;
                var innerExceptionStackTrace = ex.InnerException?.StackTrace;

                Console.WriteLine("An error occurred while updating the entries.");
                Console.WriteLine("Inner exception message: " + innerExceptionMessage);
                Console.WriteLine("Inner exception stack trace: " + innerExceptionStackTrace);
            }

            // if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            temp = 1;
            try
            {
                var applicant = _mapper.Map<Applicant>(applicantModel);
                var validationErrors = new ApplicantHandler(_applicantService).CanAdd(applicant);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
               
                if (validationResults.Any())
                {
                    temp = 2;
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    temp = 3;
                     _applicantService.Create(applicant);
                    Console.WriteLine(temp);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Applicant.ApplicantSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                temp = 4;
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            Console.WriteLine(temp);
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
