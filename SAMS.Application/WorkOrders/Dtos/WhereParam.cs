﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class WhereParam
    {
        public string SearchKey { get; set; }
        public int? Filter { get; set; }

        public string CustomerId { get; set; }
        public string SerialNo { get; set; }
    }
}
