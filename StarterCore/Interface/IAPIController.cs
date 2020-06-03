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
        IActionResult GetById(int id);
        IActionResult GetList(int id, int page = 0, int page_size = 0, string parameter = "");
        IActionResult Insert([FromBody]T Model);
        IActionResult Update([FromBody]T Model);
        IActionResult Delete(int id);
    }
}
