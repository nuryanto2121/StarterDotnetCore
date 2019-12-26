using Microsoft.AspNetCore.Mvc;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Interface
{
    public interface IAuthAPI
    {
        IActionResult Login([FromBody]AuthLogin Model);
        IActionResult Logout([FromBody]AuthLogin Model);
        IActionResult ChangePassword([FromBody]ChangePassword Model);
        IActionResult Register([FromBody]ChangePassword Model);
        IActionResult ResetPassword([FromBody]ChangePassword Model);
    }

    public interface IAuthService
    {
        Output Login(AuthLogin Model);
        Output Logout(AuthLogin Model);
        Output ChangePassword(ChangePassword Model);
    }

}
