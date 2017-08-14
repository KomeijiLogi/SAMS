using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using SAMS.Inventory.Dto;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using Abp.AutoMapper;
using SAMS.Accessories;
using SAMS.Users;
using Abp.Threading;
using SAMS.Products;
namespace SAMS.Inventory
{
    public class InventoryAppService : SAMSAppServiceBase, IInventoryAppService
    {
        private readonly IRepository<StaffStock> _staffStockRepository;
        private readonly IRepository<OrgStock> _orgStockRepository;
        private readonly IRepository<StockBill> _stockBillRepository;
        private readonly IRepository<Accessory> _accessoryRepository;
        private readonly IInventoryDomainService _inventroyDomainService;
        private readonly UserManager _userManager;
        private readonly IRepository<Bom> _bomRepository;
        public InventoryAppService(IRepository<StaffStock> staffStockRepository, IRepository<OrgStock> orgStockRepository,
            IRepository<StockBill> stockBillRepository, IRepository<Bom> bomRepository,
            UserManager userManager,IInventoryDomainService inventroyDomainService, IRepository<Accessory> accessoryRepository)
        {
            _staffStockRepository = staffStockRepository;
            _orgStockRepository = orgStockRepository;
            _stockBillRepository = stockBillRepository;
            _inventroyDomainService= inventroyDomainService;
            _accessoryRepository= accessoryRepository;
            _userManager = userManager;
            _bomRepository = bomRepository;
        }
        /// <summary>
        /// 员工持有配件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<StaffStockDto> GetStaffStock(GetNewStaffStockInput input)
        {
    
            var query = _staffStockRepository.GetAll().Where(u=>u.User.Id==input.StaffID);
  
            var orderCount = query.Count();
            var staffStockList = 
                query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();

            var staffStockListDto = staffStockList.MapTo<List<StaffStockDto>>();
            return new PagedResultDto<StaffStockDto>(
                orderCount,
                staffStockListDto
                );
        }
        public ListResultDto<StaffStockDto> GetStaffStockList(long staffId)
        {

            var query = _staffStockRepository.GetAll().Where(u => u.User.Id == staffId);

            var orderCount = query.Count();
            var staffStockList = query.ToList();

            var staffStockListDto = staffStockList.MapTo<List<StaffStockDto>>();
            return new ListResultDto<StaffStockDto>(staffStockListDto);
        }
        public ListResultDto<StaffStockDto> GetStaffStockListByProduct(long staffId,int productId)
        {
            var stock = _staffStockRepository.GetAll().Where(u => u.User.Id == staffId).Where(u=>u.Count>0);
            var bom = _bomRepository.GetAllIncluding().Where(e => e.ProductId == productId);
            var query = from s in stock
                        join b in bom on s.AccessoryId equals b.AccessoryId
                        select s;
            var orderCount = query.Count();
            var staffStockList = query.ToList();

            var staffStockListDto = staffStockList.MapTo<List<StaffStockDto>>();
            return new ListResultDto<StaffStockDto>(staffStockListDto);
        }

        public PagedResultDto<OrgStockDto> GetOrgStock(GetOrgStockInput input)
        {
            var query = _orgStockRepository.GetAll().Where(u => u.Count > 0);
            var recordCount = query.Count();
            var list = query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();
            var orgStockList = list.MapTo<List<OrgStockDto>>();
            return new PagedResultDto<OrgStockDto>(recordCount, orgStockList);
        }
        public PagedResultDto<StockBillHeadDto> GetNewExportEnterStockOrder(GetNewExportEnterStockOrderInput input)
        {
            var query = from bill in _stockBillRepository.GetAllIncluding()
                        join user in _userManager.Users on bill.CreatorUserId equals user.Id
                        select new StockBillHeadDto()
                        {
                            CategoryID = bill.CategoryID,
                            Description = bill.Description,
                            StockType = bill.StockType,
                            CreationTime = bill.CreationTime,
                            StraffName = bill.Straff.Name,
                            CreatorName = user.Name,
                            Id=bill.Id
                        };
            var recordCount = query.Count();
            var list = query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();
          
            return new PagedResultDto<StockBillHeadDto>(recordCount, list);
        }

        public void CreateNewEnterStockOrder(CreateNewEnterStockOrderInput input)
        {
            StockBill bill = new StockBill();
            bill.CategoryID = input.CategoryID;
            bill.StockType = input.StockType;
          
            User straff = null;
            if (input.CategoryID == 4)
            {
                straff = AsyncHelper.RunSync<User>(() => _userManager.GetUserByIdAsync(input.StraffID));
            }
            bill.Straff = straff;
            bill.Description = input.Description;
            bill.StockBillEntries=new  List<StockBillEntry>();
            foreach (var e in input.StockBillEntry)
            {
                var entry = new StockBillEntry();
                entry.Bill = bill;
              
                Accessory accessory = _accessoryRepository.Get(e.AccessoryId);
                entry.Accessory = accessory;
                entry.Count = e.Count;
                bill.StockBillEntries.Add(entry);
                //更新备件库存
                _inventroyDomainService.UpdateOrgInventory(accessory,e.Count);
                if(input.CategoryID==4)
                {
                    //员工退料，更新员工持有数量
                    _inventroyDomainService.UpdateStaffInventory(straff, accessory, e.Count * (-1));
                }

            }
            //保存单据
            _stockBillRepository.Insert(bill);
         
        }
        public void CreateNewExportStockOrder(CreateNewExportStockOrderInput input)
        {
            StockBill bill = new StockBill();
            bill.CategoryID = input.CategoryID;
            bill.StockType = input.StockType;
          
            User straff = null;
            if (input.CategoryID == 3)
            {
                 straff = AsyncHelper.RunSync<User>(() => _userManager.GetUserByIdAsync(input.StraffID));
            }
            bill.Straff = straff;
            bill.Description = input.Description;
            bill.StockBillEntries = new List<StockBillEntry>();
            foreach (var e in input.StockBillEntry)
            {
                var entry = new StockBillEntry();
                entry.Bill = bill;

                Accessory accessory = _accessoryRepository.Get(e.AccessoryId);
                entry.Accessory = accessory;
                entry.Count = e.Count;
                bill.StockBillEntries.Add(entry);
                //更新库存
                _inventroyDomainService.UpdateOrgInventory(accessory, e.Count*(-1));
                //员工领料，更新员工持有数量
                if (input.CategoryID == 3)
                {
                    _inventroyDomainService.UpdateStaffInventory(straff, accessory, e.Count);
                }
            }
            //保存单据
            _stockBillRepository.Insert(bill);

        }

        public  StockBillDto GetStockBillDetail(int id)
        {
            var bill = _stockBillRepository.Get(id);
            var billDto = bill.MapTo<StockBillDto>();
            var user = AsyncHelper.RunSync<User>(() => _userManager.GetUserByIdAsync(bill.CreatorUserId.Value));
           
            billDto.CreatorName = user.Name;
            return billDto;

        }
        /// <summary>
        /// 员工领料记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PagedResultDto<StockBillDto> GetNewStaffPicking(GetNewStaffPickingInput input)
        {
            var query=_stockBillRepository.GetAllIncluding().Where(e => e.StraffId == input.StaffID).Where(e=>e.CategoryID==3);
            var recordCount = query.Count();
            var list = query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();
            var billList = list.MapTo<List<StockBillDto>>();
            return new PagedResultDto<StockBillDto>(recordCount, billList);

        }
        /// <summary>
        /// 员工退料记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public PagedResultDto<StockBillDto> GetNewStaffRefund(GetNewStaffRefundInput input)
        {
            var query = _stockBillRepository.GetAllIncluding().Where(e => e.StraffId == input.StaffID).Where(e => e.CategoryID == 4);
            var recordCount = query.Count();
            var list = query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();
            var billList = list.MapTo<List<StockBillDto>>();
            return new PagedResultDto<StockBillDto>(recordCount, billList);

        }

    }
}
