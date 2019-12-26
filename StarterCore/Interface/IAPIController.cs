using Microsoft.AspNetCore.Mvc;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Interface
{
    public interface IAPIController<T>
    {        
        IActionResult GetById([FromBody]T Model);

        IActionResult GetList([FromBody]T Model);

        IActionResult Insert([FromBody]T Model);
        IActionResult Update([FromBody]T Model);

        IActionResult Delete([FromBody]T Model);
    }
}
