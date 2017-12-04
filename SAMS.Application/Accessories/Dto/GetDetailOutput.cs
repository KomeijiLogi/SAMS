using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SAMS.Users;
using Abp.AutoMapper;

namespace SAMS.Accessories.Dtos
{
    [AutoMapFrom(typeof(Accessory))]
    public class GetDetailOutput:EntityDto<string>
    {
        public virtual string Name { get; set; }
        public virtual string Model { get; set; }
        public virtual string Number { get; set; }
        public virtual string Unit { get; set; }
    }
}
