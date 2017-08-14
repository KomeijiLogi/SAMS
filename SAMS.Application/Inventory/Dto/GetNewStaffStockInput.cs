﻿using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using SAMS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class GetNewStaffStockInput: PagedAndSortedInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if(string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Accessory.Name";
            }
        }
        public long StaffID { get; set; }

    }
}
