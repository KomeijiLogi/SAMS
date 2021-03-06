﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMapFrom(typeof(ServiceEvalution))]
    public class ServiceEvalutionDto: EntityDto
    {
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
        public virtual DateTime CreationTime { get; set; }

    }
}
