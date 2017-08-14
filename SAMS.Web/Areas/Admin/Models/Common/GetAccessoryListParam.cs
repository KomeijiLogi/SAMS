using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Common
{
    public class GetAccessoryListParam
    {
        public string Type { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public string Direction { get; set; }
        public string Where { get; set; }
        public long? StaffID { get; set; }
    }
   
}