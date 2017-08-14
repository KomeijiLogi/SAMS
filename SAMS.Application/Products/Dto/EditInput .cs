using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Products.Dtos
{
    public class EditInput
    {

        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Model { get; set; }
        public string EASNumber { get; set; }
        public string K3Number { get; set; }
    }
}
