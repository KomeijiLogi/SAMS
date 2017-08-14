using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.WorkOrders
{
    public enum Direct:byte
    {
        /// <summary>
        /// 安装
        /// </summary>
        Install=1,
        /// <summary>
        /// 卸载
        /// </summary>
        Uninstall=2
    }
}
