using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory.Dto
{
    public class StockBillHeadDto
    {
        public int Id { get; set; }
        public  int StockType { get; set; }
        public string StockTypeName
        {
            get
            {
                if (this.StockType == 1)
                    return "出库单";
                else
                    return "入库单";
            }
        }
        /// <summary>
        /// 1：系统出库 2：系统入库 3：员工领料 4：员工退料
        /// </summary>
        public virtual int CategoryID { get; set; }
        public string CategoryName {
            get {
                switch (this.CategoryID)
                {
                    case 1:
                        return "系统出库";
                    case 2:
                        return "系统入库";
                    case 3:
                        return "员工领料";
                    case 4:
                        return "员工退料";

                }
                return ""; }
        }
        /// <summary>
        /// 领料/ 退料 员工员工
        /// </summary>
        public  string StraffName { get; set; }
       
        public  string Description { get; set; }

        public DateTime CreationTime { get; set; }
        public string CreatorName { get; set; }

    }
}
