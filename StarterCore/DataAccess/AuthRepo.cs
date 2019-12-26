using Dapper;
using StarterCore.Helper;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.DataAccess
{
    public class AuthRepo
    {
        private string connectionString;
        public AuthRepo(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
        public SysUser GetDataAuth(string Code,string Password)
        {
            SysUser t = null;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string strQuery = @"SELECT sys_user_id, user_id,
                                      user_name,    email,
                                      handphone,    is_active,
                                      pwd,          company_id,
                                      picture_path, user_input,
                                      user_edit,    time_input,
                                      time_edit
                                    FROM
                                      sys_user WHERE user_id = @user_id AND pwd iLIKE @pwd;";
                try
                {
                    conn.Open();
                    t = conn.Query<SysUser>(strQuery, new { user_id = Code,pwd = Password }).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }

            }
            return t;
        }
        public bool SaveUserSession(UserSession domain)
        {
            bool result = false;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO sys_user_session(user_id,token,last_login,expire_on,ip_address,user_input,user_edit) VALUES(@user_id,@token,@last_login,@expire_on,@ip_address,@user_input,@user_edit)";
                try
                {
                    conn.Open();
                    conn.Execute(sqlQuery, domain);
                    result = true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return result;
        }

        public bool SaveUserLog(SysUserLog domain)
        {
            bool result = false;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO public.sys_user_log (user_id,ip_address,login_date,logout_date,token,user_input,user_edit) VALUES (@user_id, @ip_address, @login_date, @logout_date, @token, @user_input, @user_edit)";
                try
                {
                    conn.Open();
                    conn.Execute(sqlQuery, domain);
                    result = true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return result;
        }
        public bool DeleteUserSession(UserSession domain)
        {
            bool result = false;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = @"DELETE from sys_user_session
                                    WHERE user_id iLIKE @user_id AND token = @token AND ip_address iLIKE @ip_address; ";
                try
                {
                    conn.Open();
                    conn.Execute(sqlQuery, domain);
                    result = true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return result;
        }
        public bool UpdateUserLog(string UserLogin, string Ip, string Token)
        {
            bool result = false;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = @"UPDATE 
                                      sys_user_log
                                    SET
                                      logout_date = now()::timestamp,                                     
                                      user_edit = @user_id,
                                      time_edit = now()::timestamp
                                    WHERE user_id = @user_id
                                      AND token = @token
                                      AND ip_address = @ip_address
                                    ;";
                try
                {
                    conn.Open();
                    conn.Execute(sqlQuery, new { user_id = UserLogin, token = Token, ip_address = Ip });
                    result = true;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
            return result;
        }
    }
}
