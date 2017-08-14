using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
    [Table("T_WO_ServiceEvalution")]
    public class ServiceEvalution: AuditedEntity
    {
        /// <summary>
        /// 问题是否解决
        /// </summary>
        public virtual bool Resolved { get; set; }

        /// <summary>
        /// 满意度评价
        /// </summary>
        public virtual EvaluationEnum? Evaluation { get; set; }
        /// <summary>
        /// 沟通摘要
        /// </summary>
        public virtual string ReturnVisitDesc { get; set; }
    }
}
