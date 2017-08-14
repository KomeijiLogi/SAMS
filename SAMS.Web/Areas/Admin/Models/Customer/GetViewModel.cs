using Abp.Application.Services.Dto;
using SAMS.Customers.Dtos;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Customer
{
    public class GetViewModel
    {
        public PagedResultDto<CustomerDto>  Customers{ get;set;}
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Start {
            get
            {
                return (PageIndex-1) * PageSize + 1;
            }
        }
      
        public int End
        {
            get
            {
                int end = Start + PageSize - 1;
                if (end > Customers.TotalCount) end = Customers.TotalCount;
                return end;
            }
        }
        public int PageCount {
            get
            {
                return (int)Math.Ceiling((double)Customers.TotalCount / (double)PageSize);
            }
        }
        public List<int> PageNumbers
        {
            get
            {
                int step = 5;//偏移量
                int leftNum = 0;//左边界
                int rightNum = 0;//右边界
             

                if(PageIndex-step<1)
                {
                    leftNum = 1;
                }
                else
                {
                    leftNum = PageIndex - step;
                }
                if(PageIndex+step>PageCount)
                {
                    rightNum = PageCount;
                }
                else
                {
                    rightNum = PageIndex + step;
                }

                var list = new List<int>();

                for (int i = leftNum; i <= rightNum; i++)
                {
                    list.Add(i);
                }

                return list;

            }
        }
    }
}