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

    public class SysMenuController : ControllerBase, IAPIController<SysMenu>
    {
        private IConfiguration config;
        private ICrudService<SysMenu, int> sysMenuService;
        public SysMenuController(IConfiguration configuration)
        {
            config = configuration;
            sysMenuService = new SysMenuService(configuration);
        }

        [HttpPost("SysMenu/Delete")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Delete([FromBody] SysMenu Model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("SysMenu/GetById")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult GetById([FromBody] SysMenu Model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("SysMenu/GetList")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult GetList([FromBody] SysMenu Model)
        {
            throw new NotImplementedException();
        }

        [HttpPost("SysMenu/Insert")]
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

        [HttpPost("SysMenu/Update")]
        [ProducesResponseType(typeof(Output), 200)]
        public IActionResult Update([FromBody] SysMenu Model)
        {
            throw new NotImplementedException();
        }
    }
}