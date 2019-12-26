using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Models
{
    public class SysRole
    {
        public int sys_role_id { get; set; }
        public string role_name { get; set; }
        public string user_input { get; set; }
        public string user_edit { get; set; }
        public DateTime time_input { get; set; }
        public DateTime time_edit { get; set; }
    }
}
