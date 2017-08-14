using SAMS.Customers.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class PopEditWorkOrderViewModel
    {
        public CustomerParam Customer { get; set; }
        
        public WorkOrderParam WorkOrder { get; set; }

    }
}