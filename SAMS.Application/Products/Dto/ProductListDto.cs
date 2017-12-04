using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Products.Dtos
{
    [AutoMapFrom(typeof(Product))]
    public class ProductListDto: EntityDto<string>
    {
        public string Name { get; set; }

        public string Model { get; set; }



      
        
    }
}
