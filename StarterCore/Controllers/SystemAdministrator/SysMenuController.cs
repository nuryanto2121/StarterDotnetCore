using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StarterCore.Helper;
using StarterCore.Interface;
using StarterCore.Models;
using StarterCore.Services;

namespace StarterCore.Controllers.SystemAdministrator
{
    [Route("v1/[controller]")]
    [ApiController]
    public class SysMenuController : ControllerBase, IAPIController<SysMenu>
    {
        private IConfiguration config;
        private ICrudService<SysMenu, int> sysMenuService;
        public SysMenuController(IConfiguration configuration)
        {
            config = configuration;
            sysMenuService = new SysMenuService(configuration);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult GetList(int id, int page = 0, int page_size = 0, string parameter = "")
        {
            JObject dd = new JObject();
            dd.Add("id", id);
            dd.Add("page", page);
            dd.Add("page_size", page_size);
            return Ok(dd);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Insert([FromBody] SysMenu Model)
        {
            var output = new Output();
            try
            {
                output = sysMenuService.Insert(Model);
            }
            catch (Exception ex)
            {
                output = Tools.Error(ex);
            }
            return Ok(output);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Update([FromBody] SysMenu Model)
        {
            var output = new Output();
            try
            {
                output = sysMenuService.Update(Model);
            }
            catch (Exception ex)
            {
                output = Tools.Error(ex);
            }
            return Ok(output);
        }
    }
}