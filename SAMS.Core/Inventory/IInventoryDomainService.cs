using Abp.Domain.Services;
using SAMS.Accessories;

using SAMS.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMS.Inventory
{
    public interface IInventoryDomainService:IDomainService
    {
        /// <summary>
        /// 获取用户持有的配件数量
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int GetAccessoryCountByUser(User user,Accessory accessory);
        /// <summary>
        /// 更改用户持有的配件数量
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accessory"></param>
        /// <param name="count"></param>
        void UpdateStaffInventory(User user, Accessory accessory, int count);

        
        void UpdateOrgInventory(Accessory accessory, int count);
    }
}
