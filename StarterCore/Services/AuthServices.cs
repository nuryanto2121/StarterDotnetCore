using EncryptLibrary.AES256Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StarterCore.DataAccess;
using StarterCore.Helper;
using StarterCore.Interface;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarterCore.Services
{
    public class AuthServices : IAuthService
    {
        private AuthRepo authRepo;
        private SysUserRepo UserEpro;
        IConfiguration config;
        private string _Session_Id;
        public AuthServices(IConfiguration Configuration)
        {
            authRepo = new AuthRepo(Tools.ConnectionString(Configuration));
            UserEpro = new SysUserRepo(Tools.ConnectionString(Configuration));
            config = Configuration;
        }

        public Output ChangePassword(ChangePassword Model)
        {
            var _result = new Output();
            try
            {
                if (Model.ConfirmPassword != Model.NewPassword)
                {
                    throw new Exception("New Password and Confirm Password must be same.");
                }
                Model.CurrentPassword = EncryptionLibrary.EncryptText(Model.CurrentPassword);
                var dataAuth = authRepo.GetDataAuth(Model.UserId, Model.CurrentPassword);
                if (dataAuth == null)
                {
                    throw new Exception("The user name or password is incorrect.");
                }
                dataAuth.pwd = EncryptionLibrary.EncryptText(Model.NewPassword);
                UserEpro.Update(dataAuth);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Output Login(AuthLogin Model)
        {
            var _result = new Output();
            Dictionary<string, object> ObjOutput = new Dictionary<string, object>();
            try
            {
                Model.PassLog = EncryptionLibrary.EncryptText(Model.PassLog);
                var dataAuth = authRepo.GetDataAuth(Model.UserLog, Model.PassLog);
                if (dataAuth != null)
                {
                    var expireDate = DateTime.Now.AddMinutes(config.GetValue<int>("appSetting:TokenExpire"));
                    _Session_Id = Token.GenerateToken(dataAuth, expireDate, Model, Tools.GetIpAddress());

                    // insert user session for authentication
                    var UserSession = new UserSession();
                    UserSession.user_id = dataAuth.user_id;
                    UserSession.expire_on = expireDate;
                    UserSession.token = _Session_Id;
                    UserSession.last_login = DateTime.Now;
                    UserSession.ip_address = Tools.GetIpAddress();
                    UserSession.user_input = Model.UserLog;
                    UserSession.user_edit = Model.UserLog;
                    authRepo.SaveUserSession(UserSession);

                    // Insert User Log
                    var SysUserLog = new SysUserLog();
                    SysUserLog.user_id = dataAuth.user_id;
                    SysUserLog.ip_address = Tools.GetIpAddress();
                    SysUserLog.login_date = DateTime.Now;
                    SysUserLog.token = _Session_Id;
                    SysUserLog.user_input = Model.UserLog;
                    SysUserLog.user_edit = Model.UserLog;
                    authRepo.SaveUserLog(SysUserLog);



                    dataAuth.pwd = "";
                    dataAuth.user_id = Tools.EncryptString(dataAuth.user_id);
                    ObjOutput.Add("dataUser", dataAuth);
                    ObjOutput.Add("Token", _Session_Id);
                    _result.Data = ObjOutput;// dataAuth;
                }
                else
                {
                    throw new Exception("The user name or password is incorrect.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Output Logout(AuthLogin Model)
        {
            Output _result = new Output();
            try
            {
                HttpContextAccessor Context = new HttpContextAccessor();
                var Headers = Context.HttpContext.Request.Headers;
                string Token = string.Empty;
                foreach (var key in Headers.Keys)
                {
                    if (key.ToLower() == "token")
                    {
                        Token = Headers[key].ToString();
                    }
                }

                var UserSession = new UserSession();
                UserSession.user_id = Tools.DecryptString(Model.UserLog);
                UserSession.token = Token;
                UserSession.ip_address = Tools.GetIpAddress();
                authRepo.DeleteUserSession(UserSession);

                authRepo.UpdateUserLog(Model.UserLog, Tools.GetIpAddress(), Token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
    }
}
