﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SAMS.WorkOrders.Dtos
{
    [AutoMap(typeof(WorkOrderBill))]
    public class WorkOrderEditDto
    {
        /// <summary>
        /// 工单Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        [Required]
        public virtual ServiceType ServiceType { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public virtual string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Required]
        public virtual string CustomerName { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        
        public virtual string CustomerArea { get; set; }
     
        /// <summary>
        /// 销售员
        /// </summary>
        
        public virtual string SaleMan { get; set; }
        /// <summary>
        /// 销售员电话
        /// </summary>
        /// 
        
        public virtual string SaleManPhone { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string IssueBill { get; set; }
        /// <summary>
        /// 销售公司
        /// </summary>
        public string SaleOrg { get; set; }

        /// <summary>
        /// 服务描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        [Required]
        public virtual int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        
        public virtual string ProductName { get; set; }
        /// <summary>
        /// 产品规格型号
        /// </summary>
        public virtual string ProductModel{ get; set; }

        /// <summary>
        /// 使用科室
        /// </summary>
        public virtual string Office { get; set; }
        /// <summary>
        /// 科室联系人
        /// </summary>
        public virtual string OfficePerson { get; set; }
        /// <summary>
        /// 科室联系人职务
        /// </summary>
        public virtual string OfficePosition { get; set; }
        /// <summary>
        /// 科室联系人电话
        /// </summary>
        public virtual string OfficeTel { get; set; }
        /// <summary>
        /// 科室联系人手机
        /// </summary>
        public virtual string OfficeMobile { get; set; }

        public virtual string SerialNo { get; set; }
        //装机分类
        public string InstallType { get; set; }
    }
}
