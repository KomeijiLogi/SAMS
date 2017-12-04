using SAMS.WorkOrders.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.StaffMobile.Models
{
    public class Receipt
    {
       public int BillId { get; set; }
       public string SerialNo { get; set; }

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

        /// <summary>
        /// 服务日期
        /// </summary>
        public virtual DateTime? ServiceTime { get; set; }
        /// <summary>
        /// 保修期至
        /// </summary>
        public virtual DateTime? Warrenty { get; set; }
        /// <summary>
        /// 是否在保
        /// </summary>
        public virtual bool GuaranteedState { get; set; }

        //public string CustomPhone { get; set; }
        public List<Accessory> Accessories { get; set; }
        public List<Fault> Faults { get; set; }
        public List<Photo> Photos { get; set; }


        public virtual string Faultap { get; set; }

        public virtual string Dealfa { get; set; }

        public virtual decimal TrafficUrban { get; set; }

        public virtual decimal TrafficLong { get; set; }

        public virtual decimal HotelEx { get; set; }
        
        public virtual decimal Supply { get; set; }

        public virtual decimal OtherEx { get; set; }

    }
    public class Accessory
    {
        public string AccessoryId { get; set; }
        public int Count { get; set; }
        public string NewSerial { get; set; }
        public string OldSerial { get; set; }
    }
    public class Fault
    {
        public int FaultId { get; set; }
    }
    public class Photo
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
       // public string ImgData { get; set; }
    }
}