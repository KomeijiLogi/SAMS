﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Product
{
    public class SaveBomInput
    {

        public int ProductId { get; set; }
        public List<AccessoryItem> Accessorys { get; set; }
    }
    public class AccessoryItem
    {
        public int AccessoryId { get; set; }
    }
}