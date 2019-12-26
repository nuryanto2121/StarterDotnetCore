using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Models
{
    public class SysMenuRigth
    {
        public int sys_menu_right_id { get; set; }
        public int sys_menu_id { get; set; }
        public int sys_role_id { get; set; }
        public bool add_status { get; set; }
        public bool edit_status { get; set; }
        public bool delete_status { get; set; }
        public bool view_status { get; set; }
        public bool post_status { get; set; }
        public string user_input { get; set; }
        public string user_edit { get; set; }
        public DateTime time_input { get; set; }
        public DateTime time_edit { get; set; }
    }
}
