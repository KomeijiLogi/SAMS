using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Staff
{
    public class Staff
    {
        public int? ID { set; get; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName{ get; set; }
        public string Mobile { get; set; }
    }
}