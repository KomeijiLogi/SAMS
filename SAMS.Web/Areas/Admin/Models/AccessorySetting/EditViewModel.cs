using Abp.Application.Services.Dto;
using SAMS.Accessories.Dtos;
using SAMS.Customers.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.AccessorySetting
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Unit { get; set; }
        public string Number { get; set; }
    }
}