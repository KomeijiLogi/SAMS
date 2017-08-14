using SAMS.Customers.Dtos;
using SAMS.Web.Areas.Admin.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Customer
{
    public class DetailsViewModel
    {
        public GetDetailOutput Customer { get; set; }
        public DateTime? LastServiceTime { get; set; }
        //public GetListViewModel WorkOrders { get; set; }
    }
}