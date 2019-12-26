using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StarterCore.DataAccess;
using StarterCore.Helper;
using StarterCore.Interface;
using StarterCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Services
{
    public class SysMenuService : ICrudService<SysMenu, int>
    {
        private SysMenuRepo SysMenuRepo;
        public SysMenuService(IConfiguration configuration)
        {
            SysMenuRepo = new SysMenuRepo(Tools.ConnectionString(configuration));
        }

        public Output Delete(int Key)
        {
            Output _result = new Output();
            try
            {
                _result.Data = SysMenuRepo.GetById(Key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Output GetDataBy(int Key)
        {
            Output _result = new Output();
            try
            {
                _result.Data = SysMenuRepo.GetById(Key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Output GetList(JObject JModel)
        {
            throw new NotImplementedException();
        }

        public Output Insert(SysMenu Model)
        {
            Output _result = new Output();
            try
            {
                Model.User_edit = Tools.DecryptString(Model.User_edit);
                Model.user_input = Tools.DecryptString(Model.user_input);
                SysMenuRepo.Save(Model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Output Update(SysMenu Model)
        {
            Output _result = new Output();
            try
            {
                Model.time_edit = DateTime.Now;
                Model.User_edit = Tools.DecryptString(Model.User_edit);
                SysMenuRepo.Update(Model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
    }
}
