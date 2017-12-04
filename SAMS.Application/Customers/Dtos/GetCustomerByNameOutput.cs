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
        public string Id { get; set; }
        public string Code { get; set; }


        public string Name { get; set; }

        public string ProvinceName { get; set; }
        public string ProvinceId { get; set; }
        public string CityName { get; set; }
        public string CityId { get; set; }

        public string Address { get; set; }
        public string Area
        {
            get
            {
                return ProvinceName + CityName;
            }
        }
    }
}
