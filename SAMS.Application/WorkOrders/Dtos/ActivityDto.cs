using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(Activity))]
    public class ActivityDto:EntityDto
    {
        //处理人员
        public string  OperaterName { get; set; }

        public  int BillId { get; set; }
        //服务人员
        public string AssignedPersonName { get; set; }
        public virtual string Log { get; set; }
        public virtual string Name { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
