using Abp.AutoMapper;
using SAMS.WorkOrders;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    [AutoMapFrom(typeof(GetDetailOutput))]
    public class ReportModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary> 
        public  string CustomerPhone { get; set; }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public  string CustomerLinkMan { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public  string SerialNo { get; set; }
        /// <summary>
        /// 使用科室
        /// </summary>
        public  string Office { get; set; }
        /// <summary>
        /// 科室联系人
        /// </summary>
        public  string OfficePerson { get; set; }
        /// <summary>
        /// 科室联系人职务
        /// </summary>
        public  string OfficePosition { get; set; }
        /// <summary>
        /// 科室联系人电话
        /// </summary>
        public  string OfficeTel { get; set; }
        /// <summary>
        /// 科室联系人手机
        /// </summary>
        public  string OfficeMobile { get; set; }

        /// <summary>
        /// 服务日期
        /// </summary>
        public  DateTime? ServiceTime { get; set; }
        /// <summary>
        /// 保修期至
        /// </summary>
        public virtual DateTime? Warrenty { get; set; }
        /// <summary>
        /// 是否在保
        /// </summary>
        public  bool GuaranteedState { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public  List<string> Photos { get; set; }
      
        //故障现象
        public  string Faultap { get; set; }

        //处理方案
        public  string Dealfa { get; set; }

        //市内交通
        [RegularExpression(@"^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$", ErrorMessage = "格式不正确！")]
        public  decimal? TrafficUrban { get; set; }

        //长途交通
        [RegularExpression(@"^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$", ErrorMessage = "格式不正确！")]
        public  decimal? TrafficLong { get; set; }

        //住宿费
        [RegularExpression(@"^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$", ErrorMessage = "格式不正确！")]
        public  decimal? HotelEx { get; set; }

        //补助
        [RegularExpression(@"^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$", ErrorMessage = "格式不正确！")]
        public  decimal? Supply { get; set; }
        //其他费用
        [RegularExpression(@"^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$", ErrorMessage = "格式不正确！")]
        public  decimal? OtherEx { get; set; }

    }
}