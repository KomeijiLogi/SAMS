using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class PopCreateParam
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}