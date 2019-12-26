using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Models
{
    public class SysUserRole
    {
        public int sys_user_role_id { get; set; }
        public int sys_user_id { get; set; }
        public int sys_role_id { get; set; }
        public string user_input { get; set; }
        public string user_edit { get; set; }
    }
}
