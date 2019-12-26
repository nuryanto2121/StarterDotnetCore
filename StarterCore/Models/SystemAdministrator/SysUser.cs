using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Models
{
    public class SysUser : BaseEntity
    {       
        public int sys_user_id { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string email { get; set; }
        public string handphone { get; set; }
        public bool is_active { get; set; }
        public string pwd { get; set; }
        public int company_id { get; set; }
        public string picture_path { get; set; }
        public string user_input { get; set; }
        public string user_edit { get; set; }
        public DateTime time_input { get; set; }
        public DateTime time_edit { get; set; }

    }
   
}
