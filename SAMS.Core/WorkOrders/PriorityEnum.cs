using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
    public enum PriorityEnum
    {
        [Display(Name="一般")]
        General=1,
        [Display(Name = "紧急")]
        Emergency =2,
        [Display(Name = "非常紧急")]
        VeryUrgent =3

    }
}
