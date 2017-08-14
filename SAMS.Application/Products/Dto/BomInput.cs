using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Products.Dtos
{
    public class BomInput
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AccessoryId { get; set; }
    }
}
