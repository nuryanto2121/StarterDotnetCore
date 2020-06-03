using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StarterCore.Models
{
    public class SysMenu : BaseEntity
    {

        public int sys_menu_id { get; set; }

        [Required(ErrorMessage = "Please enter Title"), MaxLength(60)]
        public string title { get; set; }
        public string url { get; set; }
        public int parent_menu_id { get; set; }
        public string parent_menu_title { get; set; }
        public string icon_class { get; set; }
        //public string path { get; set; }
        //[Required]
        public int order_seq { get; set; }

        public string user_input { get; set; }

        public string User_edit { get; set; }

        public DateTime time_input { get; set; }

        public DateTime time_edit { get; set; }
        public int lastupdatestamp { get; set; }
    }
}
