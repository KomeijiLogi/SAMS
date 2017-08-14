using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.AccessoryInventoryBills
{
    public enum InventoryBillType
    {
        [Description("备件入库")]
        In = 1,
        [Description("备件出库")]
        Out=2,
        [Description("员工领料")]
        Req = 3,
        [Description("员工退料")]
        Back = 4

    }
}
