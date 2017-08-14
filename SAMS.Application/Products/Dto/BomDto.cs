using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
namespace SAMS.Products.Dtos
{
    [AutoMapFrom(typeof(Bom))]
    public class BomDto
    {
        public int Id { get; set;}
        public int ProductId { get; set; }
        public int AccessoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string AccessoryName { get; set; }
        public string AccessoryModel { get; set; }
    }
}
