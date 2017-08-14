using Abp.AutoMapper;
using SAMS.Accessories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Accessories.Dtos
{
    [AutoMapFrom(typeof(Accessory))]
    public class GetAccessoryDto
    {
        public int AccessoryId { get; set; }
        public  string AccessoryName { get; set; }
        public  string AccessoryModel { get; set; }
        public  string AccessoryUnit { get; set; }
        public  string AccessoryNumber { get; set; }
        
    }
}
