using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory.Dto
{
    [AutoMapFrom(typeof(StockBillEntry))]
    public class StockBillEntryDto
    {
       
   
        public  int AccessoryId { get; set; }
        public  int Count { get; set; }
        public  string AccessoryName { get; set; }
        public string AccessoryNumber { get; set; }
        public string AccessoryModel { get; set; }
        public string AccessoryUnit { get; set; }
    
    }
}
