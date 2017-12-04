using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAMS.WorkOrders;
namespace SAMS.Web.Areas.StaffMobile.Models
{
    public class CreateOrUpdateOrder
    {
        public int? Id { get; set;}
        public string CustomerId { get; set; }
        public  string CustomerName { get; set; }
       
        public string ProductId { get; set; }
        public ServiceType ServiceType{ get; set; }
        public string Description { get; set; }
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

     
    }
}