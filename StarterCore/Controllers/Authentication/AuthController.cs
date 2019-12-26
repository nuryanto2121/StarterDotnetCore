using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StarterCore.Helper;
using StarterCore.Interface;
using StarterCore.Models;
using StarterCore.Services;

namespace StarterCore.Controllers.Authentication
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AuthController : ControllerBase, IAuthAPI
    {
        private IAuthService AuthService;
        public AuthController(IConfiguration configuration)
        {
            AuthService = new AuthServices(configuration);
        }

        [AllowAnonymous]
        [HttpPost("Auth/ChangePassword")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult ChangePassword([FromBody] ChangePassword Model)
        {
            var _result = new Output();
            try
            {

            }
            catch (Exception ex)
            {
                _result = Tools.Error(ex);
            }
            return this.Ok(_result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Auth/Login")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Login([FromBody] AuthLogin Model)
        {
            var _result = new Output();
            try
            {
                _result = this.AuthService.Login(Model);
            }
            catch (Exception ex)
            {
                _result = Tools.Error(ex, 401);
                //JObject Out = JObject.Parse("{'Status':500,'Error':'true','Data':null,'Message':'" + _result.Message + "'}");
                //return new HttpActionResult(HttpStatusCode.Unauthorized, Out);
            }
            return this.Ok(_result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Auth/Logout")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Logout([FromBody] AuthLogin Model)
        {
            var _result = new Output();
            try
            {

            }
            catch (Exception ex)
            {
                _result = Tools.Error(ex);
            }
            return this.Ok(_result);
        }


        [AllowAnonymous]
        [HttpPost("Auth/Register")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Register([FromBody] ChangePassword Model)
        {
            var _result = new Output();
            try
            {

            }
            catch (Exception ex)
            {
                _result = Tools.Error(ex);
            }
            return this.Ok(_result);
        }

        [AllowAnonymous]
        [HttpPost("Auth/Reset")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult ResetPassword([FromBody] ChangePassword Model)
        {
            var _result = new Output();
            try
            {

            }
            catch (Exception ex)
            {
                _result = Tools.Error(ex);
            }
            return this.Ok(_result);
        }
    }
}