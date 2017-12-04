using SAMS.Products.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Models.EquipmentArchive
{
    public class ArchiveModel
    {
        
        /// <summary>
        /// 产品
        /// </summary>
        [DisplayName("产品")]
        [Required(ErrorMessage = "产品不能为空")]
        public string ProductId { get; set; }
        public SelectList PersonItemList;
        public IEnumerable<ProductListDto> Products { get; set; }

        public List<SelectListItem> ProductItemList
        {
            get
            {
                
                var selectItemList = new List<SelectListItem>();
                if (Products == null)
                    return selectItemList;

                var Items = this.Products.Select(x => new ProductListDto()
                {
                    Name = x.Name+" "+x.Model,
                    Id=x.Id
                }).ToList();
                selectItemList.AddRange(new SelectList(Items, "Id", "Name"));

                return selectItemList;
            }

        }
        /// <summary>
        /// 产品序列号
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// 使用科室
        /// </summary>
        [Display(Name ="使用科室")]
        [StringLength(50,ErrorMessage ="{0}的长度不能超过{1}")]
        public string Office { get; set; }
        /// <summary>
        /// 科室联系人
        /// </summary>
        [Display(Name = "科室联系人")]
        [StringLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string OfficePerson { get; set; }
        /// <summary>
        /// 科室联系人职务
        /// </summary>
        [Display(Name = "联系人职务")]
        [StringLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string OfficePosition { get; set; }
        /// <summary>
        /// 科室联系人电话
        /// </summary>
        [Display(Name = "联系人电话")]
        [StringLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public virtual string OfficeTel { get; set; }
        /// <summary>
        /// 科室联系人手机
        /// </summary>
        [Display(Name = "联系人手机")]
        [RegularExpression(@"^[1][3-8]\d{9}$", ErrorMessage = "{0}格式不正确")]
        public string OfficeMobile { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        public DateTime? ServiceTime { get; set; }


        /// <summary>
        /// 保修期至
        /// </summary>
        public DateTime? Warrenty { get; set; }

        /// <summary>
        /// 接单人
        /// </summary>

        public long? AssignedPersonId { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerId { get; set; }

        //装机分类
        public string InstallType { get; set; }
    }
}