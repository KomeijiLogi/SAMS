using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Customers.Dtos
{
    [AutoMapFrom(typeof(Customer))]
    public class GetCustomerByNameDto
    {
        public string Name { get; set; }
        public string Id { get; set; }
      
        public virtual string Area { get; set; }//黑龙江省-大庆市-肇州镇

       
        public virtual string Address { get; set; }
        
        public virtual string Mobile { get; set; }

        public virtual string Email { get; set; }
    }
}
