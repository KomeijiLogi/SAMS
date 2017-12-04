using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using SAMS.Areas;
using Abp.Domain.Entities;

namespace SAMS.Customers
{
   
    [Table("t_bd_Customer")]
    public class Customer : Entity<string>
        {
            public const int MaxNumberLength = 500;
            public const int MaxNameLength = 500;
            public const int MaxAddressLength = 1000;

            [StringLength(MaxNumberLength)]
            public virtual string Code { get; set; }

            [StringLength(MaxNameLength)]
            public virtual string Name { get; set; }

            public virtual string ProvinceName { get; set; }
            public virtual string ProvinceId { get; set; }
            public virtual string CityName { get; set; }
            public virtual string CityId { get; set; }

            [StringLength(MaxAddressLength)]
            public virtual string Address { get; set; }


        }
}
