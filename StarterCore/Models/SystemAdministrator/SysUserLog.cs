﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Models
{
    public class SysUserLog
    {
        public int sys_user_log_id { get; set; }
        public string user_id { get; set; }
        public string ip_address { get; set; }
        public DateTime login_date { get; set; }
        public DateTime logout_date { get; set; }
        public string token { get; set; }
        public string user_input { get; set; }
        public string user_edit { get; set; }
        public DateTime time_input { get; set; }
        public DateTime time_edit { get; set; }
    }
}
