using Abp.Domain.Entities.Auditing;
using SAMS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory
{
    [Table("T_inv_StockBill")]
    public class StockBill: FullAuditedEntity
    {
        /// <summary>
        /// 1：出库 2：入库
        /// </summary>
        public virtual int  StockType{ get; set; }
        /// <summary>
        /// 1：系统出库 2：系统入库 3：员工领料 4：员工退料
        /// </summary>
        public virtual int CategoryID { get; set; }

        /// <summary>
        /// 领料/ 退料 员工员工
        /// </summary>
        [ForeignKey("StraffId")]
        public virtual User Straff { get; set; }
        public virtual long? StraffId { get; set; }
        [StringLength(150)]
        public virtual String Description { get; set; }

   
        public virtual ICollection<StockBillEntry> StockBillEntries { get; set; }
        public StockBill()
        {
            //this.StockBillEntries = new List<StockBillEntry>();
        }
    }
}
