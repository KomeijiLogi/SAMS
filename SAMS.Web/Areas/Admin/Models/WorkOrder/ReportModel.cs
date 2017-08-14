using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class ReportModel
    {
        public GetDetailOutput WorkOrder { get; set; }
        public string StaffStocks{get;set;}
    }
}