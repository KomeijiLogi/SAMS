using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Customer
{
    public class EditViewModel
    {
       public int Id { get; set; }
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }


        public string Number1 { get; set; }


        public string Number2 { get; set; }

        [Required(ErrorMessage = "区域不能为空")]
        public string Area { get; set; }//黑龙江省-大庆市-肇州镇


        public string Address { get; set; }


        public string Description { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// EMail
        /// </summary>
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }
    }
}