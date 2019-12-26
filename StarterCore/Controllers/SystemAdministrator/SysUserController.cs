using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StarterCore.Models;

namespace StarterCore.Controllers
{
    public class SysUserController : ControllerBase
    {
        private IConfiguration config;
        public SysUserController (IConfiguration configuration)
        {
            config = configuration;
        }
        /// <summary>
        /// Save User with data Post
        /// </summary>
        /// <remarks>
        /// here sampe remark placeholder
        /// </remarks>
        /// <param name="Model"> </param>
        /// <returns> with Output Object</returns>
        [AllowAnonymous]
        [HttpPost("SysUser/Add")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Login([FromBody]SysUser Model)
        {
            var output = new Output();
            output.Data = Model;

            return Ok(output);
        }

        /// <summary>
        /// tatatat asdfsd
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("SysUser/Get")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Get(int Key)
        {
            var output = new Output();
            output.Data = new { name = "Yanto", age = Key };

            return Ok(output);
        }

        /// <summary>
        /// this api dynamic with parameter like in SP
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        [HttpPost("SysUser/Insert")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult AAA([FromBody]JObject Key)
        {
            var output = new Output();
            output.Data = Key;

            return Ok(output);
        }
    }
}