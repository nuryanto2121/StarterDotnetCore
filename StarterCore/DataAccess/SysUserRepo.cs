using Dapper;
using StarterCore.Enum;
using StarterCore.Helper;
using StarterCore.Interface;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterCore.DataAccess
{
    public class SysUserRepo : IRepository<SysUser, int>
    {
        private string connectionString;
        public SysUserRepo(string ConnectionString)
        {
            connectionString = ConnectionString;
        }
        public object SelectScalar(SQL.Function.Aggregate function, string column)
        {
            object _result = null;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                switch (function)
                {
                    case SQL.Function.Aggregate.Max:
                        sbQuery.AppendFormat("SELECT MAX({0}) FROM public.sys_user ", column);
                        break;
                    case SQL.Function.Aggregate.Min:
                        sbQuery.AppendFormat("SELECT MIN({0}) FROM public.sys_user ", column);
                        break;
                    case SQL.Function.Aggregate.Distinct:
                        sbQuery.AppendFormat("SELECT DISTINCT({0}) FROM public.sys_user ", column);
                        break;
                    case SQL.Function.Aggregate.Count:
                        sbQuery.AppendFormat("SELECT COUNT({0}) FROM public.sys_user ", column);
                        break;
                    case SQL.Function.Aggregate.Sum:
                        sbQuery.AppendFormat("SELECT SUM({0}) FROM public.sys_user ", column);
                        break;
                    case SQL.Function.Aggregate.Avg:
                        sbQuery.AppendFormat("SELECT AVG({0}) FROM public.sys_user ", column);
                        break;
                    default:
                        // do nothing 
                        break;
                }

                try
                {
                    conn.Open();
                    _result = conn.ExecuteScalar(sbQuery.ToString());
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
            return _result;
        }
        public List<SysUser> GetList()
        {
            List<SysUser> tt = new List<SysUser>();
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                try
                {
                    string sQuery = @"SELECT 
                                          sys_user_id,    user_id,
                                          user_name,      email,
                                          handphone,      is_active,
                                          company_id,     picture_path,
                                          user_input,     user_edit,                                          
                                          time_input,     time_edit
                                        FROM 
                                          public.sys_user ;";
                    conn.Open();
                    tt = conn.Query<SysUser>(sQuery).ToList();
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
            return tt;
        }
        public List<SysUser> GetList(int start, int pageSize, string sortName, string sortOrder, string Parameter)
        {
            List<SysUser> tt = new List<SysUser>();
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                int startRow = (start + 1);
                int endRow = (start + pageSize);

                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendFormat(" WITH result_set AS ");
                sbQuery.AppendFormat(" ( ");
                sbQuery.AppendFormat("    SELECT ");
                sbQuery.AppendFormat("      ROW_NUMBER() OVER (ORDER BY {0} {1}) AS [row_number], ", sortName, sortOrder);
                sbQuery.AppendFormat(@"    sys_user_id,    user_id,
                                          user_name,      email,
                                          handphone,      is_active,
                                          company_id,     picture_path,
                                          user_input,     user_edit,                                          
                                          time_input,     time_edit     ");
                sbQuery.AppendFormat("    FROM ");
                sbQuery.AppendFormat("      public.sys_user  ");
                sbQuery.AppendFormat(" {0} ", Parameter);
                sbQuery.AppendFormat(" ) ");
                sbQuery.AppendFormat(" SELECT * FROM result_set WHERE [row_number] BETWEEN {0} AND {1} ", startRow, endRow);

                try
                {
                    conn.Open();
                    tt = conn.Query<SysUser>(sbQuery.ToString()).ToList<SysUser>();
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

            return tt;
        }
        public SysUser GetById(int key)
        {
            SysUser t = null;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string strQuery = @"SELECT sys_user_id,    user_id,
                                          user_name,      email,
                                          handphone,      is_active,
                                          company_id,     picture_path,
                                          user_input,     user_edit,                                          
                                          time_input,     time_edit
                                 FROM public.sys_user  WHERE sys_menu_id = @sys_menu_id";
                try
                {
                    conn.Open();
                    t = conn.Query<SysUser>(strQuery, new { sys_menu_id = key }).SingleOrDefault();
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
        public bool Save(SysUser domain)
        {
            bool result = false;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = @"INSERT INTO 
                                      public.sys_user
                                    (
                                      sys_user_id,user_id,
                                      user_name,email,
                                      handphone,is_active,
                                      pwd,company_id,
                                      picture_path,user_input,
                                      user_edit,time_input,
                                      time_edit
                                    )
                                    VALUES (
                                      @sys_user_id,@user_id,
                                      @user_name,@email,
                                      @handphone,@is_active,
                                      @pwd,@company_id,
                                      @picture_path,@user_input,
                                      @user_edit,@time_input,
                                      @time_edit
                                    );";
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
        public bool Update(SysUser domain)
        {
            int result = 0;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = @"UPDATE 
                                  public.sys_user 
                                SET 
                                  user_id = @user_id,
                                  user_name = @user_name,
                                  email = @email,
                                  handphone = @handphone,
                                  is_active = @is_active,
                                  pwd = @pwd,
                                  company_id = @company_id,
                                  picture_path = @picture_path,
                                  user_input = @user_input,
                                  user_edit = @user_edit,
                                  time_input = @time_input,
                                  time_edit = @time_edit
                                WHERE sys_user_id = @sys_user_id;";
                //AND xmin::text::integer = @lastupdatestamp";

                try
                {
                    conn.Open();
                    result = conn.Execute(sqlQuery, domain);
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
            return (result > 0);
        }
        public bool Delete(int key)
        {
            int result = 0;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM public.sys_user WHERE sys_user_id = @sys_user_id";
                try
                {
                    conn.Open();
                    result = conn.Execute(sqlQuery, new { sys_menu_id = key });
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
            return (result > 0);
        }
    }
}
