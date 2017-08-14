using SAMS.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory.Dto
{
    public class CreateNewExportStockOrderInput
    {
        public  int StockType { get; set; }
        public  int CategoryID { get; set; }
        /// <summary>
        /// 退料员工
        /// </summary>
        public  long StraffID { get; set; }
        public  String Description { get; set; }
        public  ICollection<StockBillEntryDto> StockBillEntry { get; set; }

    }
}
