using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.EquipmentArchive
{
    public class ArchiveEditParm
    {
        public int Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        
        public string ProductId { get; set; }
       
        /// <summary>
        /// 产品序列号
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
        public virtual string OfficeTel { get; set; }
        /// <summary>
        /// 科室联系人手机
        /// </summary>
        public  string OfficeMobile { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        public  DateTime? ServiceTime { get; set; }

        
        /// <summary>
        /// 保修期至
        /// </summary>
        public  DateTime? Warrenty { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>
        
        public  long? AssignedPersonId { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public  string CustomerId { get; set; }

        //装机分类
        public  string InstallType { get; set; }
    }
}