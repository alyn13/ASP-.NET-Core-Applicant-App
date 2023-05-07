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
using static BaseCode.Data.Constants;
using Constants = BaseCode.Data.Constants;
using Exception = System.Exception;

namespace BaseCode.API.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.Common.Bearer, Roles = Constants.Roles.Admin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HighSchoolAPIController : ControllerBase
    {
        private readonly IHighSchoolService _highschoolService;
        private readonly IMapper _mapper;

        public HighSchoolAPIController(IHighSchoolService highschoolService, IMapper mapper)
        {
            _highschoolService = highschoolService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Applicant record.
        /// </summary>
        /// <param name="id">ID of the Applicant record</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("getHighSchool")]
        public HttpResponseMessage GetHighSchool(int id)
        {
            var highschool = _highschoolService.Find(id);
            return highschool != null ? Helper.ComposeResponse(HttpStatusCode.OK, highschool) : Helper.ComposeResponse(HttpStatusCode.NotFound, HighSchool.HighSchoolDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of HighSchool records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding HighSchool records</param>
        /// <returns></returns>
       /* [HttpGet]
        [AllowAnonymous]
        [ActionName("list")]
        public HttpResponseMessage GetApplicantList([FromQuery] HighSchoolSearchViewModel searchModel)
        {
            var responseData = _highschoolService.FindHighSchools(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }
       */
        /// <summary>
        ///     This function adds a Applicant record.
        /// </summary>
        /// <param name="highschoolModel">Contains Applicant properties</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ActionName("add")]
        public HttpResponseMessage PostHighSchool(HighSchoolViewModel highschoolModel)
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
                var highschool = _mapper.Map<HighSchoolEducation>(highschoolModel);
                var validationErrors = new HighSchoolHandler(_highschoolService).CanAdd(highschool);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    temp = 2;
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    temp = 3;
                    _highschoolService.Create(highschool);
                    Console.WriteLine(temp);
                    return Helper.ComposeResponse(HttpStatusCode.OK, HighSchool.HighSchoolSuccessAdd);
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
        /// <param name="highschoolModel">Contains Applicant properties</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("edit")]
        [AllowAnonymous]
        public HttpResponseMessage PutHighSchool(HighSchoolViewModel highschoolModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var highschool = _mapper.Map<HighSchoolEducation>(highschoolModel);
                var validationErrors = new HighSchoolHandler(_highschoolService).CanUpdate(highschool);
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

                    _highschoolService.Update(highschool);
                    return Helper.ComposeResponse(HttpStatusCode.OK, HighSchool.HighSchoolSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function deletes an HighSchool record.
        /// </summary>
        /// <param name="id">ID of the HighSchool record</param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous] //change later
        [ActionName("delete")]
        public HttpResponseMessage DeleteHighSchool(int id)
        {
            try
            {
                var validationErrors = new HighSchoolHandler(_highschoolService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _highschoolService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, HighSchool.HighSchoolSuccessDelete);
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
