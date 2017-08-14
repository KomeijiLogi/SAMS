using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
    /// <summary>
    /// 单据状态
    /// </summary>
    public enum BillStatus
    {
        

        [Description("新建")]
        Save =1,

        [Description("已受理")]
        Accept =2,

        [Description("已分配")]
        Assinged =3,

        [Description("已派工")]
        Dispatch =4,

        [Description("已完工")]
        Complete =5,

        [Description("已结算")]
        Statment = 6,

        [Description("已回访")]
        ReturnVisit = 7,

        [Description("已关闭")]
        Close = 8,

        [Description("已取消")]
        Cancel =9

    }
}
