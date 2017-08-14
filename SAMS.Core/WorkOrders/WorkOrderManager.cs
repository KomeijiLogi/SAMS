using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SAMS.Users;
using Abp;
using Abp.Domain.Uow;
using SAMS.Accessories;

namespace SAMS.WorkOrders
{
    public class WorkOrderManager : IWorkOrderManager
    {
        private readonly IRepository<WorkOrderBill> _workOrderRepository;
        private readonly IRepository<WorkOrderFaultEntry> _workOrderFaultEntryRepository;
        private readonly UserManager _userManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<Activity> _activityRepository;

        public WorkOrderManager(IRepository<WorkOrderBill> workOrderRepository,
            IRepository<WorkOrderFaultEntry> workOrderFaultEntryRepository,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Activity> activityRepository
            )
        {
            _workOrderRepository = workOrderRepository;
            _workOrderFaultEntryRepository = workOrderFaultEntryRepository;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
            _activityRepository = activityRepository;
        }
        
        /// <summary>
        /// 新建工单
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public void  Create(WorkOrderBill bill)
        {
            bill.BillStatus = BillStatus.Save;
            _workOrderRepository.InsertAndGetId(bill);
        }

        /// <summary>
        /// 提交工单
        /// </summary>
        /// <param name="bill"></param>
        public void Submit(WorkOrderBill bill)
        {
            BeforeSubmit(bill);
            _workOrderRepository.InsertOrUpdate(bill);

        }


        /// <summary>
        /// 下达工单
        /// </summary>
        /// <param name="bill"></param>
        public void Release(WorkOrderBill bill)
        {
            //下达前校验
            BeforeDispatchAsync(bill);
            bill.BillStatus = BillStatus.Dispatch;
            bill.DispatchTime = DateTime.Now;
            _unitOfWorkManager.Current.Completed += (sender, args) => {
                /* TODO: 给接单人发送待办消息*/

            };

        }
        public void Complete(WorkOrderBill bill)
        {
            
            bill.BillStatus = BillStatus.Complete;
            //var workOrder=_workOrderRepository.GetAllIncluding()
            //    .Where(e => e.Id == bill.Id)
            //    .FirstOrDefault();
            //workOrder.FaultEntry.Add(new WorkOrderFaultEntry { ParentId = bill.Id, FaultId = 3 });
            //var en=workOrder.FaultEntry.FirstOrDefault(x => x.Id == 3);
            //_workOrderFaultEntryRepository.Delete(en);
            //workOrder.FaultEntry.Remove(en);

        }
      

        /// <summary>
        /// 提交前处理
        /// </summary>
        /// <param name="bill"></param>
        private  void BeforeSubmit(WorkOrderBill bill)
        {

            if(bill.BillStatus.Equals(BillStatus.Save )||bill.BillStatus.Equals(BillStatus.Accept))
            { }
            else
            {
                throw new AbpException("只有保存和提交状态的工单才能提交");
            }
        }
       
        /// <summary>
        /// 审核前处理
        /// </summary>
        /// <param name="bill"></param>
        private void BeforeAudit(WorkOrderBill bill)
        {
            if(bill.BillStatus.Equals(BillStatus.Accept))
            {

            }
            else
            {
                throw new Exception("工单没有提交禁止审核");
            }
        }
        
        /// <summary>
        /// 派工前处理
        /// </summary>
        /// <param name="bill"></param>
        private async void  BeforeDispatchAsync(WorkOrderBill bill)
        {
            //只有审核和完工汇报状态下的工单才能下达
            if(bill.BillStatus.Equals(BillStatus.Accept)
                || bill.BillStatus.Equals(BillStatus.Dispatch))
            {}
            else
            {
                throw new AbpException("只有受理和派工状态的单据可以派工");
            }

            if(bill.AssignedPerson==null)
            {
                throw new AbpException("没有指定接单人");
            }

            var user =  await _userManager.GetUserByIdAsync(bill.AssignedPersonId.Value);
           
            if(!user.IsActive)
            {
                throw new AbpException("接单人没有激活");
            }
        }
        /// <summary>
        /// 根据id取得工单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WorkOrderBill> GetAsync(int id)
        {
            var workOrderBill = await _workOrderRepository.GetAsync(id);
            if(workOrderBill==null)
            {
                throw new AbpException("单据不存在");
            }
            return workOrderBill;
            
        }

        public WorkOrderBill Get(int id)
        {
            var workOrderBill =  _workOrderRepository.Get(id);
            if (workOrderBill == null)
            {
                throw new AbpException("单据不存在");
            }
            return workOrderBill;

        }

        public void Accept(WorkOrderBill bill)
        {
            bill.BillStatus = BillStatus.Accept;
        }

        public void Dispatch(WorkOrderBill bill, User user)
        {
            bill.BillStatus = BillStatus.Dispatch;
            bill.DispatchTime = DateTime.Now;
            bill.AssignedPerson = user;
        }

        public void AddAccessory(WorkOrderBill bill, Accessory accessory, int count, int backCount)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccessory(WorkOrderAccessoryEntry id)
        {
            throw new NotImplementedException();
        }

        public void ReturnVisit(WorkOrderBill bill)
        {
            bill.BillStatus = BillStatus.Close;
        }

        public void Close(WorkOrderBill bill)
        {
            throw new NotImplementedException();
        }

        public void Cancel(WorkOrderBill bill)
        {
            bill.BillStatus = BillStatus.Cancel;
        }

        public WorkOrderAccessoryEntry AddAccessoryEntry(WorkOrderBill bill, Accessory accessory, int count, int backCount)
        {
            throw new NotImplementedException();
        }

        public void DeleteAccessoryEntry(WorkOrderAccessoryEntry entry)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccessoryEntry(WorkOrderAccessoryEntry entry)
        {
            throw new NotImplementedException();
        }

        public void AddActivity(Activity activity)
        {
            _activityRepository.InsertAndGetId(activity);
        }
    }
}
