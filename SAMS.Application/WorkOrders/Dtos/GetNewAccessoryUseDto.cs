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
    public class GetNewAccessoryUseDto
    {
      //  public int Id { get; set; }
        
        public DateTime CreationTime { get; set; }

        // public virtual int ParentId { get; set; }
        public  long StaffID { get; set; }
        public  string AccessoryId { get; set; }
        public  string AccessoryName { get; set; }
        public  string AccessoryNumber { get; set; }
        public  string AccessoryModel { get; set; }
        public  string AccessoryUnit { get; set; }
        public  int Count { get; set; }
        public  int BackCount { get; set; }

        //public  ICollection<WorkOrderAccessoryDto> AccessoryEntry { get; set; }
    }
}
