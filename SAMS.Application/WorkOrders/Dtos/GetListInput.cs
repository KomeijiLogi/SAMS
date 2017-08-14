using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class GetListInput : PagedResultRequestDto
    {
       
        public WorkOrderListType QueryType { get; set; }
        public string KeyWord{ get; set; }

    }
    public enum WorkOrderListType 
    {

        /// <summary>
        /// 待处理
        /// </summary>
        Undo=1,
        /// <summary>
        /// 已完工
        /// </summary>
        Done=2
      
    }
}
