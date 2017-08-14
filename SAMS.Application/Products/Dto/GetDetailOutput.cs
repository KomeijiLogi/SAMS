using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SAMS.Users;
using Abp.AutoMapper;

namespace SAMS.Products.Dtos
{
    [AutoMapFrom(typeof(Product))]
    public class GetDetailOutput:EntityDto
    {
        public virtual string Name { get; set; }

        public virtual string Model { get; set; }
        public virtual string EASNumber { get; set; }
        public virtual string K3Number { get; set; }
    }
}
