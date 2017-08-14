using Abp.AutoMapper;
using SAMS.WorkOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderBill))]
    public class GetNewStaffUseDto
    {
        public int Id { get; set; }
        
        public DateTime CreationTime { get; set; }
        public  ICollection<WorkOrderAccessoryDto> AccessoryEntry { get; set; }
    }
}
