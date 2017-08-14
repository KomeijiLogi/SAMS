using SAMS.Products.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class CreateOrEditWorkOrderModalViewModel
    {
       
        public WorkOrderEditDto WorkOrder { get; set; }
        public ProductListDto[] Products { get; set; }
        public List<SelectListItem>  ProductItemList
        {
            get
            {
                var selectItemList = new List<SelectListItem>();
                //{
                //    new SelectListItem(){Value="",Text="选择产品名称",Selected=WorkOrder == null}
                //}; 
                selectItemList.AddRange(new SelectList(Products, "Name", "Name", WorkOrder == null ? "": WorkOrder.ProductId.ToString()));
                return selectItemList;
            }
            
        }
        public List<SelectListItem> ModelItemList
        {
            get
            {
                var selectItemList = new List<SelectListItem>();
                //{
                //    new SelectListItem(){Value="",Text="选择产品型号",Selected=WorkOrder == null }
                //};
                if (WorkOrder != null)
                { 
                    var models = Products
                        .Where(e => e.Name == WorkOrder.ProductName)
                        .Select(e => e.Model)
                        .ToList();

                    selectItemList.AddRange(new SelectList(models, "Model", "Model", 
                        WorkOrder == null ? "" : WorkOrder.ProductModel));
                }
                return selectItemList;
            }

        }

        public bool IsEditMode
        {
            get { return WorkOrder!=null && WorkOrder.Id.HasValue; }
        }
    }
}