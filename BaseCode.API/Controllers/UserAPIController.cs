using AutoMapper;
using BaseCode.Data;
using BaseCode.Data.Models;
using BaseCode.Data.ViewModels;
using BaseCode.Domain.Contracts;
using BaseCode.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Constants = BaseCode.Data.Constants;

namespace BaseCode.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAPIController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        [ActionName("listAllUsers")]
        public HttpResponseMessage GetStudentList([FromQuery] UserAdminViewModel searchModel)
        {
            var responseData = _userService.FindAllUsers(searchModel);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("register")]
        public async Task<HttpResponseMessage> PostRegister(UserViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            var result = await _userService.RegisterUser(userModel.UserName, userModel.Password, userModel.FirstName, userModel.LastName, userModel.EmailAddress, userModel.RoleName);
            var errorResult = GetErrorResult(result);

            return errorResult ? Helper.ComposeResponse(HttpStatusCode.BadRequest, ModelState) : Helper.ComposeResponse(HttpStatusCode.OK, Constants.User.ReegisterSuccess);
        }
        [HttpPut]
        [ActionName("updateAdmin")]
        public HttpResponseMessage PutAdmin(UserUpdateViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }

            var result = _userService.Update(userModel);

            if (result == true) return Helper.ComposeResponse(HttpStatusCode.OK, "Successfully updated user");
            else return Helper.ComposeResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [AllowAnonymous]
        [HttpDelete]
        [ActionName("deleteAdmin")]
        public HttpResponseMessage DeleteAdmin(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.DeleteById(id);
                    return Helper.ComposeResponse(HttpStatusCode.OK, "Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("roles")]
        public async Task<HttpResponseMessage> PostCreateRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Constants.Common.InvalidRole);
            }

            var result = await _userService.CreateRole(role);
            var errorResult = GetErrorResult(result);

            return errorResult ? Helper.ComposeResponse(HttpStatusCode.BadRequest, Constants.Common.InvalidRole) : Helper.ComposeResponse(HttpStatusCode.OK, "Successfully added role");
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("login")]
        public async Task<HttpResponseMessage> Login(UserLoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return Helper.ComposeResponse(HttpStatusCode.BadRequest, Helper.GetModelStateErrors(ModelState));
            }
            var result = await _userService.FindUserAsync(userLogin.UserName, userLogin.Password);
            if (result == null) return Helper.ComposeResponse(HttpStatusCode.BadRequest, Constants.User.InvalidUserNamePassword);


            return Helper.ComposeResponse(HttpStatusCode.OK, Constants.User.LoginSuccess);
        }
        /*
        [AllowAnonymous]
        [HttpPost]
        [ActionName("view_users")]
        public async Task<HttpResponseMessage> ViewAllUsers(UserLoginViewModel userLogin)
        {
            //var responseData = _userService.FindAll(userLogin);
            return Helper.ComposeResponse(HttpStatusCode.OK, responseData);
        }
        */

        private bool GetErrorResult(IdentityResult result)
        {
            if (result.Succeeded || result.Errors == null) return false;

            var flag = false;
            foreach (var error in result.Errors)
            {
                flag = true;
                ModelState.AddModelError("ModelStateErrors", error.Description);
            }

            return flag;
        }
    }
}