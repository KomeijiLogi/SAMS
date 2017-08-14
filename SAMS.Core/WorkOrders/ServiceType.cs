using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public enum ServiceType:byte
    {

        
        [Display(Name="安装")]
        Install = 1,
        [Display(Name="维修")]
        Repair = 2
        
    }
}
