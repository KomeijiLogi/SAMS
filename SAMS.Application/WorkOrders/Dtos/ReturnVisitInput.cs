using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    public class ReturnVisitInput
    {
        public virtual int BillId { get; set; }
        /// <summary>
        /// 问题是否解决
        /// </summary>
        public virtual bool Resolved { get; set; }

        /// <summary>
        /// 满意度评价
        /// </summary>
        public virtual EvaluationEnum Evaluation { get; set; }
        /// <summary>
        /// 沟通摘要
        /// </summary>
        public virtual string ReturnVisitDesc { get; set; }
    }
}
