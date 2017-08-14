﻿using SAMS.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class CreateViewModel
    {
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public ServiceType ServiceTypeID { get; set; }
        public string Description { get; set; }
        public PriorityEnum Priority { get; set; }
        public string CustomerPhone { get; set; }
        public string SaleMan { get; set; }
        public string SaleManPhone { get; set; }
    }
}