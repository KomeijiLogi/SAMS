using SAMS.Products.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class PopEditViewModel
    {
        public WorkOrderEditDto WorkOrder { get; set; }
        public ProductListDto[] Products { get; set; }
        public IEnumerable<SelectListItem> ProductItemList
        {
            get
            {
                //var selectItemList = new List<SelectListItem>();
                ////{
                ////    new SelectListItem(){Value="",Text="选择产品名称",Selected=WorkOrder == null}
                ////}; 
                //selectItemList.AddRange(new SelectList(Products, "Name", "Name", "选择产品名称"));
                //return selectItemList;

                var selectItems = Products.Distinct(new ProductNameComparer()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name,
                    Selected = x.Name == WorkOrder.ProductName
                });
                return selectItems;
            }

        }
        public IEnumerable<SelectListItem> ProductTypeItemList
        {
            get
            {
                var selectItems = Products
                    .Where(e => e.Name == WorkOrder.ProductName)
                    .Select(x => new SelectListItem
                    {

                        Text = x.Model,
                        Value = x.Model,
                        Selected = x.Model == WorkOrder.ProductModel
                    });
                return selectItems;
            }
        }
        private class ProductNameComparer : IEqualityComparer<ProductListDto>
        {
            public bool Equals(ProductListDto x, ProductListDto y)
            {
                if (x.Name == y.Name)
                {
                    return true;
                }
                else
                    return false;
            }
            public int GetHashCode(ProductListDto obj)
            {
                return 0;
            }
        }
    }
}