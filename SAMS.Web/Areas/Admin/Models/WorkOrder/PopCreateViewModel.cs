using SAMS.Products.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Models.WorkOrder
{
    public class PopCreateViewModel
    {
       
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string SaleMan { get; set; }
        public string SaleManPhone { get; set; }
        public ProductListDto[] Products { get; set; }
        public List<SelectListItem>  ProductItemList
        {
            get
            {
                var selectItemList = new List<SelectListItem>();
                //{
                //    new SelectListItem(){Value="",Text="选择产品名称",Selected=WorkOrder == null}
                //}; 

                var Items = Products.Select(x => new ProductListDto()
                {
                    Name = x.Name
                    //Model=x.Model

                }).ToList();

                //HashSet<ProductListDto> hs = new HashSet<ProductListDto>(Items);


                for (int i=0;i<Items.Count;i++) {
                    for (int j= Items.Count-1;j>i;j--) {

                        if (Items[i].Name==Items[j].Name) {
                            Items.RemoveAt(j);
                        }
                    }

                }



                //selectItemList.AddRange(new SelectList(Products, "Name", "Name", "选择产品名称"));
                selectItemList.AddRange(new SelectList(Items, "Name", "Name", "选择产品名称"));
               
                return selectItemList;
            }
            
        }
       

        
    }
}