using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SAMS.Users;
using Abp.AutoMapper;

namespace SAMS.Customers.Dtos
{
    [AutoMapFrom(typeof(Customer))]
    public class GetDetailOutput:EntityDto<string>
    {

        public virtual string Code { get; set; }

      
        public virtual string Name { get; set; }

        public virtual string ProvinceName { get; set; }
        public virtual string ProvinceId { get; set; }
        public virtual string CityName { get; set; }
        public virtual string CityId { get; set; }


        public virtual string Address { get; set; }






    }
}
