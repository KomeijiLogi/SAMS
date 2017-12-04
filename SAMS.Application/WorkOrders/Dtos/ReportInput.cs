
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace SAMS.WorkOrders.Dtos
{
    
    public class ReportInput
    {
        /// <summary>
        ///单据id 
        /// </summary>
        public  int BillId { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public  string SerialNo { get; set; }

        
        /// <summary>
        /// 照片
        /// </summary>
        public  List<string> Photos { get; set; }

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
        /// 保修期至
        /// </summary>
        public  DateTime? Warrenty { get; set; }
        /// <summary>
        /// 是否在保
        /// </summary>
        public  bool GuaranteedState { get; set; }
        /// <summary>
        /// 服务日期
        /// </summary>
        public  DateTime? ServiceTime { get; set; }


        //故障现象
        public  string Faultap { get; set; }

        //处理方案
        public  string Dealfa { get; set; }


        //市内交通
        public decimal? TrafficUrban { get; set; }

        //长途交通
        public decimal? TrafficLong { get; set; }

        //住宿费
        public  decimal? HotelEx { get; set; }

        //补助
        public  decimal? Supply { get; set; }
        //其他费用
        public  decimal? OtherEx { get; set; }



    }
}