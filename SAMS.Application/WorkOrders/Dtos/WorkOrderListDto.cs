using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(WorkOrderBill))]
    public class WorkOrderListDto : EntityDto<int>, IHasCreationTime
    {

        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public  string SaleMan { get; set; }
        public  ServiceType ServiceType { get; set; }
        public  string AssignedPersonName { get; set; }
        public  PriorityEnum Priority { get; set; }
        public  BillStatus BillStatus { get; set; }
        public DateTime? DispatchTime { get; set; }
        public DateTime CreationTime
        {
            get;set;
           
        }
    }
}
