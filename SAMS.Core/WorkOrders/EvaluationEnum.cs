using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
    /// <summary>
    /// 满意度评价
    /// </summary>
    public enum EvaluationEnum
    {
        [Description("非常满意")]
        Exellent = 5,

        [Description("满意")]
        Good = 4,

        [Description("一般")]
        Normal = 3,

        [Description("不满意")]
        Worse = 2,

        [Description("非常不满意")]
        Worst = 1


    }
}
