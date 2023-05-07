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

namespace BaseCode.API.Controllers
{ 
    [Authorize(AuthenticationSchemes = Constants.Common.Bearer, Roles = Constants.Roles.Admin)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobAPIController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobAPIController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        /// <summary>
        ///     This function retrieves a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("getJob")]
        [AllowAnonymous]
        public HttpResponseMessage GetJob(int id)
        {
            var job = _jobService.Find(id);
            return job != null ? Helper.ComposeResponse(HttpStatusCode.OK, job) : Helper.ComposeResponse(HttpStatusCode.NotFound, Constants.Job.JobDoesNotExists);
        }

        /// <summary>
        ///     This function retrieves a list of Student records.
        /// </summary>
        /// <param name="searchModel">Search filters for finding Student records</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        [AllowAnonymous]
        public HttpResponseMessage GetJobList([FromQuery] JobSearchViewModel searchModel)
        {
            var responseData = _jobService.FindJobs(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }

        /// <summary>
        ///     This function adds a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("add")]
        [AllowAnonymous]
        public HttpResponseMessage PostJob(JobViewModel jobModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));

            try
            {
                var job = _mapper.Map<Job>(jobModel);
                var validationErrors = new JobHandler(_jobService).CanAdd(job);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    /*if (claimsIdentity != null)
                    {
                        job.CreatedBy = claimsIdentity.Name;
                        job.CreatedDate = DateTime.Now;
                    }*/

                    _jobService.Create(job);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Job.JobSuccessAdd);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function updates a Student record.
        /// </summary>
        /// <param name="studentModel">Contains Student properties</param>
        /// <returns></returns>
        [HttpPut]
        [ActionName("edit")]
        [AllowAnonymous]
        public HttpResponseMessage PutJob(JobViewModel jobModel)
        {
            if (!ModelState.IsValid) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            try
            {
                var job = _mapper.Map<Job>(jobModel);
                var validationErrors = new JobHandler(_jobService).CanUpdate(job);
                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();

                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    var claimsIdentity = User.Identity as ClaimsIdentity;
                    if (claimsIdentity != null)
                    {
                        job.ModifiedBy = claimsIdentity.Name;
                        job.ModifiedDate = DateTime.Now;
                    }

                    _jobService.Update(job);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Job.JobSuccessEdit);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        /// <summary>
        ///     This function deletes a Student record.
        /// </summary>
        /// <param name="id">ID of the Student record</param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("delete")]
        [AllowAnonymous]
        public HttpResponseMessage DeleteJob(int id)
        {
            try
            {
                var validationErrors = new JobHandler(_jobService).CanDelete(id);

                var validationResults = validationErrors as IList<ValidationResult> ?? validationErrors.ToList();
                if (validationResults.Any())
                {
                    ModelState.AddModelErrors(validationResults);
                }

                if (ModelState.IsValid)
                {
                    _jobService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, Constants.Job.JobSuccessDelete);
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