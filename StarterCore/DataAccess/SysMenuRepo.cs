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
    public class SysMenuRepo : IRepository<SysMenu, int>
    {
        private string connectionString;
        public SysMenuRepo(string ConnectionString)
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
                        sbQuery.AppendFormat("SELECT MAX({0}) FROM public.sys_menu ", column);
                        break;
                    case SQL.Function.Aggregate.Min:
                        sbQuery.AppendFormat("SELECT MIN({0}) FROM public.sys_menu ", column);
                        break;
                    case SQL.Function.Aggregate.Distinct:
                        sbQuery.AppendFormat("SELECT DISTINCT({0}) FROM public.sys_menu ", column);
                        break;
                    case SQL.Function.Aggregate.Count:
                        sbQuery.AppendFormat("SELECT COUNT({0}) FROM public.sys_menu ", column);
                        break;
                    case SQL.Function.Aggregate.Sum:
                        sbQuery.AppendFormat("SELECT SUM({0}) FROM public.sys_menu ", column);
                        break;
                    case SQL.Function.Aggregate.Avg:
                        sbQuery.AppendFormat("SELECT AVG({0}) FROM public.sys_menu ", column);
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
        public List<SysMenu> GetList()
        {
            List<SysMenu> tt = new List<SysMenu>();
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                try
                {
                    string sQuery = @"SELECT 
                                      sys_menu_id, title,
                                      url, parent_menu_id,
                                      parent_menu_title, icon_class,
                                      path, order_seq,
                                      user_input, user_edit,
                                      time_input, time_edit
                                    FROM 
                                      public.sys_menu ;";
                    conn.Open();
                    tt = conn.Query<SysMenu>(sQuery).ToList();
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
        public List<SysMenu> GetList(int start, int pageSize, string sortName, string sortOrder,string Parameter)
        {
            List<SysMenu> tt = new List<SysMenu>();
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                int startRow = (start + 1);
                int endRow = (start + pageSize);

                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendFormat(" WITH result_set AS ");
                sbQuery.AppendFormat(" ( ");
                sbQuery.AppendFormat("    SELECT ");
                sbQuery.AppendFormat("      ROW_NUMBER() OVER (ORDER BY {0} {1}) AS [row_number], ", sortName, sortOrder);
                sbQuery.AppendFormat("        sys_menu_id,  title,  url,  parent_menu_id,  parent_menu_title,  icon_class,  path,  order_seq,  user_input,  user_edit,  time_input,  time_edit ");
                sbQuery.AppendFormat("    FROM ");
                sbQuery.AppendFormat("      public.sys_menu  ");
                sbQuery.AppendFormat(" {0} ",Parameter);
                sbQuery.AppendFormat(" ) ");
                sbQuery.AppendFormat(" SELECT * FROM result_set WHERE [row_number] BETWEEN {0} AND {1} ", startRow, endRow);

                try
                {
                    conn.Open();
                    tt = conn.Query<SysMenu>(sbQuery.ToString()).ToList<SysMenu>();
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
        public SysMenu GetById(int key)
        {
            SysMenu t = null;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string strQuery = "SELECT sys_menu_id,  title,  url,  parent_menu_id,  parent_menu_title,  icon_class,  path,  order_seq,  user_input,  user_edit,  time_input,  time_edit FROM public.sys_menu  WHERE sys_menu_id = @sys_menu_id";
                try
                {
                    conn.Open();
                    t = conn.Query<SysMenu>(strQuery, new { sys_menu_id = key }).SingleOrDefault();
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
        public bool Save(SysMenu domain)
        {
            bool result = false;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = @"INSERT INTO 
                                      public.sys_menu
                                    ( title,  url,
                                      parent_menu_id,  parent_menu_title,
                                      icon_class,  path,
                                      order_seq,  user_input,  user_edit
                                    )
                                    VALUES (
                                      @title,  @url,
                                      @parent_menu_id,  @parent_menu_title,
                                      @icon_class,  @path,
                                      @order_seq,  @user_input,  @user_edit
                                    )";
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
        public bool Update(SysMenu domain)
        {
            int result = 0;
            using (IDbConnection conn = Tools.DBConnection(connectionString))
            {
                string sqlQuery = @"UPDATE public.sys_menu 
                                SET 
                                  title = @title,
                                  url = @url,
                                  parent_menu_id = @parent_menu_id,
                                  parent_menu_title = @parent_menu_title,
                                  icon_class = @icon_class,
                                  path = @path,
                                  order_seq = @order_seq,
                                  user_edit = @user_edit,
                                  time_edit = @time_edit
                                WHERE 
                                  sys_menu_id = @sys_menu_id";

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
                string sqlQuery = "DELETE FROM public.sys_menu WHERE sys_menu_id = @sys_menu_id";
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
