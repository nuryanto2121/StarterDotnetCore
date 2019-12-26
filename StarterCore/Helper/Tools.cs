using EncryptLibrary.AES256Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Npgsql;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarterCore.Helper
{
    public class Tools
    {
        /// <summary>
        /// ini utk connection postgress
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static IDbConnection DBConnection(string ConnectionString)
        {
            return new NpgsqlConnection(ConnectionString);
        }
        /// <summary>
        /// Ini utk Connecti SQL
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static IDbConnection SqlConnection(string ConnectionString)
        {
            return new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Connection Mysql
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static IDbConnection MySqlConnection(string ConnectionString)
        {
            return new MySqlConnection(ConnectionString);
        }
        public static string UserId
        {
            get
            {
                HttpContextAccessor dd = new HttpContextAccessor();
                var Headers = dd.HttpContext.Request.Headers;
                string Token = string.Empty;
                foreach (var key in Headers.Keys)
                {
                    if (key.ToLower() == "token")
                    {
                        Token = Headers[key].ToString();
                    }
                }
                var Key = EncryptionLibrary.DecryptText(Token);
                string[] Parts = Key.Split(new string[] { "~!@#$%" }, StringSplitOptions.None);
                return Parts[0];
            }

        }
        public static string DecryptString(string data)
        {
            string _result = data;
            if (isBase64(data))
            {
                _result = EncryptionLibrary.DecryptText(data);
            }
            return _result;
        }
        public static string EncryptString(string data)
        {

            return EncryptionLibrary.EncryptText(data);

        }
        public static bool isBase64(string Base64)
        {
            bool v;
            try
            {
                EncryptionLibrary.DecryptText(Base64);
                v = true;
            }
            catch (Exception ex)
            {
                v = false;
            }
            return v;
        }
        public static string ConnectionString(IConfiguration configuration)
        {
            return configuration.GetValue<string>("appSetting:ConnectionString");
        }
        public static string GetIpAddress()
        {
            HttpContextAccessor dd = new HttpContextAccessor();
            return dd.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        public static Output Error(Exception ex, int statusCode = 500)
        {
            Output op = new Output();
            op.Message = ex.Message;
            op.Status = statusCode;
            op.Error = true;
            return op;
        }
        public static Output Error(string ex)
        {

            Output op = new Output();
            op.Message = ex;
            op.Status = 500;
            op.Error = true;
            return op;
        }

        public static Exception SetError(string msg, HttpStatusCode code)
        {
            var ex = new Exception(string.Format("{0} - {1}", msg, code));
            ex.Data.Add(code, msg);
            return ex;
        }
    }
    public static class Token
    {
        public static string GenerateToken(SysUser dataUser, DateTime ExpireToken, AuthLogin Log, string IpAddress)
        //public static string GenerateToken(DataTable dtTableLogin, DateTime ExpireToken, AuthLog Log)
        {
            try
            {
                string randomnumber = string.Join("~!@#$%", new string[] {
                    dataUser.user_id,
                    EncryptionLibrary.KeyGenerator.GetUniqueKey(),
                    dataUser.pwd,
                    IpAddress,
                    Convert.ToString(DateTime.Now.Ticks),
                    Convert.ToString(ExpireToken.Ticks),
                    dataUser.email
                    //dataUser.Rows[0]["user_group"].ToString()

                });

                return EncryptionLibrary.EncryptText(randomnumber);
                //return SymmCrypto.Encrypt(randomnumber, PAMSCrypto.CryptWord);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
