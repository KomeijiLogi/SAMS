using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAMS.Users;
using SAMS.Accessories;
using Abp.Domain.Repositories;

namespace SAMS.Inventory
{
    public class InventoryDomainService : IInventoryDomainService
    {
        private readonly IRepository<OrgStock> _orgStockRepository;
        private readonly IRepository<StaffStock> _staffStockRepository;
        public InventoryDomainService(IRepository<OrgStock> orgStockRepository, IRepository<StaffStock> staffStockRepository)
        {
            _orgStockRepository = orgStockRepository;
            _staffStockRepository = staffStockRepository;
        }

        public void UpdateStaffInventory(User user, Accessory accessory, int count)
        {

            StaffStock stock = _staffStockRepository
                .GetAll()
                .Where(x => x.AccessoryId == accessory.Id)
                .Where(x => x.UserId == user.Id)
                .FirstOrDefault();
            if(stock==null)
            {
                _staffStockRepository.Insert(new StaffStock() { Accessory = accessory, User = user, Count = count });
            }
            else
            {
                stock.Count = stock.Count + count;
            }
        }

        public int GetAccessoryCountByUser(User user, Accessory accessory)
        {
            var query = _staffStockRepository.GetAll()
                .Where(e => e.AccessoryId == accessory.Id)
                .Where(e => e.UserId == user.Id);
            var staffstock = query.SingleOrDefault();
            return staffstock == null ? 0 : staffstock.Count;

        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessory"></param>
        /// <param name="count">增加或减少的数量</param>
        public void  UpdateOrgInventory(Accessory accessory,int count)
        {
            OrgStock stock=_orgStockRepository.GetAll().Where(x => x.AccessoryId == accessory.Id).FirstOrDefault();
            if(stock==null)
            {
                _orgStockRepository.Insert(new OrgStock() { Accessory = accessory, Count = count });
            }
            else
            {
                stock.Count = stock.Count + count;
            }
        }
    }
}
