using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Data.ViewModels.Common;
using BaseCode.Domain;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Handlers;
using BaseCode.Domain.Services;
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
using Website = BaseCode.Data.Models.Website;

namespace BaseCode.API.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.Common.Bearer, Roles = Constants.Roles.Admin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebsiteAPIController : ControllerBase
    {
        private readonly IWebsiteService _websiteService;
        private readonly IMapper _mapper;

        public WebsiteAPIController(IWebsiteService websiteService, IMapper mapper)
        {
            _websiteService = websiteService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Applicant record.
        /// </summary>
        /// <param name="id">ID of the Applicant record</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("getWebsite")]
        public HttpResponseMessage GetWebsite(int id)
        {
            var website = _websiteService.Find(id);
            return website != null ? Helper.ComposeResponse(HttpStatusCode.OK, website) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Website.WebsiteDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Applicant records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Applicant records</param>
        /// <returns></returns>
        /*[HttpGet]
        [AllowAnonymous]
        [ActionName("list")]
        public HttpResponseMessage GetWebsiteList([FromQuery] WebsiteSearchViewModel searchModel)
        {
            var responseData = _websiteService.FindWebsitets(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }
        */
        /// <summary>
        ///     This function adds a Applicant record.
        /// </summary>
        /// <param name="websiteModel">Contains Applicant properties</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ActionName("add")]
        public HttpResponseMessage PostWebsite(WebsiteViewModel websiteModel)
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
                var website = _mapper.Map<Website>(websiteModel);
                var validationErrors = new WebsiteHandler(_websiteService).CanAdd(website);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    temp = 2;
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    temp = 3;
                    _websiteService.Create(website);
                    Console.WriteLine(temp);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Website.WebsiteSuccessAdd);
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
        /// <param name="websiteModel">Contains Applicant properties</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("edit")]
        [AllowAnonymous]
        public HttpResponseMessage PutWebsite(WebsiteViewModel websiteModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var website = _mapper.Map<Website>(websiteModel);
                var validationErrors = new WebsiteHandler(_websiteService).CanUpdate(website);
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

                    _websiteService.Update(website);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Website.WebsiteSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function deletes an Website record.
        /// </summary>
        /// <param name="id">ID of the Website record</param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous] //change later
        [ActionName("delete")]
        public HttpResponseMessage DeleteWebsite(int id)
        {
            try
            {
                var validationErrors = new WebsiteHandler(_websiteService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _websiteService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Website.WebsiteSuccessDelete);
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
