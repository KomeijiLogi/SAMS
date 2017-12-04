using SAMS.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Models.EquipmentArchive
{
    public class IndexViewModel
    {
        public ProductListDto[] Products { get; set; }
        public List<SelectListItem> ProductItemList
        {
            get
            {
                var selectItemList = new List<SelectListItem>();
                var Items = Products.Select(x => new ProductListDto()
                {
                    Name = x.Name+"_"+x.Model,
                    Id=x.Id

                }).ToList();
                selectItemList.AddRange(new SelectList(Items, "Id", "Name", "选择产品"));
                return selectItemList;
            }

        }
    }
}