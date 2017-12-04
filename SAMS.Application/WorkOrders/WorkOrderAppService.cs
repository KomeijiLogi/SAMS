using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAMS.WorkOrders.Dtos;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using Abp.Application.Services.Dto;
using SAMS.EntityFramework;
using System.Data.Entity;
using Abp.UI;
using SAMS.Users;
using Abp.Authorization;
using SAMS.Authorization;
using Abp.Application.Services;
using SAMS.Customers;
using SAMS.Areas;
using Abp.Threading;

using SAMS.Inventory;
using SAMS.Accessories;
using System.Data.Entity.Core.Objects;
using YUN;
using System.Configuration;
using Newtonsoft.Json;
using SAMS.Products;
using SAMS.EquipmentArchives;
using SAMS.EquipmentArchives.Dto;

namespace SAMS.WorkOrders
{
    public class WorkOrderAppService : SAMSAppServiceBase, IWorkOrderAppService
    {
        private readonly IWorkOrderManager _workOrderManager;
        private readonly IRepository<WorkOrderBill> _workOrderRepository;
        private readonly IRepository<Accessory,string> _accessoryRepository;
        private readonly IRepository<WorkOrderFaultEntry> _workOrderFaultEntryRepository;
        private readonly IRepository<Customer,string> _customerRepository;
        private readonly IRepository<ServiceEvalution> _serviceEvalutionRepository;
        private readonly IRepository<WorkOrderAccessoryEntry> _workOrderAccessoryEntryRepository;
        private readonly IRepository<WorkOrderPhoto> _workOrderPhotoRepository;
        private readonly ICustomerDomainService _customerDomainService;
        private readonly IRepository<Product, string> _productRepository;
        private readonly IEquipmentArchiveAppService _equipmentArchiveAppService;
        //  private readonly AccessoryDomainService _accessoryDomainService;

        private readonly IInventoryDomainService _inventoryDomainService;
        public WorkOrderAppService(IWorkOrderManager workOrderManager,
            IRepository<WorkOrderBill> workOrderRepository,
            IRepository<WorkOrderFaultEntry> workOrderFaultEntryRepository,
            IRepository<WorkOrderAccessoryEntry> workOrderAccessoryEntryRepository,
            ICustomerDomainService customerDomainService,
            IRepository<Customer,string> customerRepository,
            IInventoryDomainService inventroyDomainService,
           IRepository<ServiceEvalution> serviceEvalutionRepository,
           IRepository<Accessory,string> accessoryRepository,
           IRepository<WorkOrderPhoto> workOrderPhotoRepository,
             IRepository<Product, string> productRepository,
             IEquipmentArchiveAppService equipmentArchiveAppService


            )
        {
            _workOrderManager = workOrderManager;
            _workOrderRepository = workOrderRepository;
            _workOrderFaultEntryRepository = workOrderFaultEntryRepository;
            _workOrderAccessoryEntryRepository = workOrderAccessoryEntryRepository;
            _customerDomainService = customerDomainService;
            _customerRepository = customerRepository;
            _inventoryDomainService = inventroyDomainService;
            _serviceEvalutionRepository = serviceEvalutionRepository;
            _accessoryRepository = accessoryRepository;
            _workOrderPhotoRepository = workOrderPhotoRepository;
            _productRepository = productRepository;
            _equipmentArchiveAppService = equipmentArchiveAppService;
        }

        /// <summary>
        /// 受理
        /// </summary>
        /// <param name="id"></param>
        public void Accept(int id)
        {

            var workOrder = _workOrderManager.Get(id);
            Accept(workOrder);

        }
        private void Accept(WorkOrderBill workOrder)
        {
            _workOrderManager.Accept(workOrder);
            //记录受理活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var acceptActivity = new Activity() { Bill = workOrder, Operater = user, Name = "受理", Log = string.Format("【{0}】受理了工单。", user.Name) };
            _workOrderManager.AddActivity(acceptActivity);
        }

        /// <summary>
        /// 派工,重新派工
        /// </summary>
        /// <param name="input"></param>
        public void Dispatch(DispatchInput input)
        {   

            var workOrder = _workOrderManager.Get(input.BillId);
            var assingedUser = AsyncHelper.RunSync(() => UserManager.FindByIdAsync(input.UserId));
            if (workOrder.BillStatus == BillStatus.Dispatch)
            {
                //记录重新派工活动
                var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
                var redispatchActivity = new Activity() { Bill = workOrder, Operater = user, AssignedPerson = assingedUser, Name = "重新派工"
                , Log = string.Format("【{0}】指派服务人员【{1}】处理该工单。", user.Name, assingedUser.Name)
                };
                _workOrderManager.AddActivity(redispatchActivity);
            }
            else
            {
                //记录派工活动
                var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
                var dispatchActivity = new Activity() { Bill = workOrder, Operater = user, AssignedPerson = assingedUser, Name = "派工"
                    , Log = string.Format("【{0}】指派服务人员【{1}】处理该工单。", user.Name, assingedUser.Name) };
                _workOrderManager.AddActivity(dispatchActivity);
            }
            //派工
            _workOrderManager.Dispatch(workOrder, assingedUser);

            string serverstr = "";
            if (((int)workOrder.ServiceType)==1) {

                serverstr = "安装";
            } else if (((int)workOrder.ServiceType) == 2) {
                serverstr = "维修";

            }
            else {

            }


            //云之家消息推送
            string bootURL = ConfigurationManager.AppSettings["bootURL"];
            string pubKey = ConfigurationManager.AppSettings["pubKey"];
            string no = ConfigurationManager.AppSettings["no"];
            string pub = ConfigurationManager.AppSettings["pub"];
            string time = DateTime.Now.CurrentTimeMillis().ToString();
            string nonce = Guid.NewGuid().ToString();
            string appID = ConfigurationManager.AppSettings["appID"];//轻应用注册到云之家时生成
            string appSecret = ConfigurationManager.AppSettings["appSecret"];//轻应用注册到云之家时生成
            string yunxt = ConfigurationManager.AppSettings["yunxt"];
            YUNAPI.YUNXT = yunxt;
            string pubtoken = YUNAPI.CreatePubToken(no, pub, pubKey, nonce, time);
            var message = new
            {
                from = new
                {
                    no = no,
                    pub = pub,
                    time = time,
                    nonce = nonce,
                    pubtoken = pubtoken
                },
                to = new object[]{
                        new {
                            no= no,
                            user=new string[] { assingedUser.UserName },
                            code=YUNAPI.PUBCODE_ACCOUNT
                            }
                    },
                type = YUNAPI.PUBTYPE_TEXTANDHREF,
                msg = new
                {
                    url = string.Format("{0}/staffMobile/home/detail/{1}",bootURL,workOrder.Id),
                    appid = appID,
                    todo = YUNAPI.TODO_YES,
                    text = serverstr + " || "+workOrder.Customer.Name ,
                    todoPriStatus = YUNAPI.TODOSTAUS_UNDO,//只能是undo  待办
                    extendFields = new { pushTipTitle = "您有一条新工单" }
                }
            };
            string json = JsonConvert.SerializeObject(message);
            try {
                var sendResut = AsyncHelper.RunSync(() => YUNAPI.PubSend(json));
                if (sendResut.success)
                    workOrder.YUNMsg = sendResut.message;
            }
            catch
            {

            }


        }
        
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="id"></param>
        public void Cancel(int id)
        {
            var workOrder = _workOrderManager.Get(id);
            _workOrderManager.Cancel(workOrder);
            //记录取消工单活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var cancelActivity = new Activity() { Bill = workOrder, Operater = user, Name = "取消", Log = string.Format("【{0}】已将工单取消。", user.Name) };
            _workOrderManager.AddActivity(cancelActivity);
        }
        /// <summary>
        /// 更新差旅费用
        /// </summary>
        /// <param name="input"></param>
        public void UpdateExpense(ExpenseInput input)
        {

            var bill = _workOrderManager.Get(input.BillId);

            bill.TrafficUrban = input.TrafficUrban;
            bill.TrafficLong = input.TrafficLong;
            bill.Supply = input.Supply;
            bill.OtherEx = input.OtherEx;
            bill.HotelEx = input.HotelEx;
        }

        /// <summary>
        /// 完工回单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Complete(CompleteInput input)
        {
            CompleteOutput output = new CompleteOutput() { Successed = true };

            var bill = _workOrderManager.Get(input.BillId);
            var archive=_equipmentArchiveAppService.GetArchiveBySerialNo(bill.SerialNo);
            if (bill.ServiceType.Equals(ServiceType.Install))
            {
                if (archive != null)
                    throw new UserFriendlyException("序列号已存在");
            }
            else
            {
                if (archive == null)
                    throw new UserFriendlyException("序列号不存在");
            }

            //记录回单活动
            if (bill.BillStatus == BillStatus.Dispatch)
            {
                var user=AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
                var reportActivity = new Activity() { Bill=bill,Operater=user,Name="回单",Log=string.Format("服务人员【{0}】已将问题处理完毕。",user.Name) };
                _workOrderManager.AddActivity(reportActivity);
            }
            //回单基本信息
            bill.SerialNo = input.SerialNo;
            bill.Office = input.Office;
            bill.OfficeMobile = input.OfficeMobile;
            bill.OfficePerson = input.OfficePerson;
            bill.OfficePosition = input.OfficePosition;
            bill.OfficeTel = input.OfficeTel;
            bill.Warrenty = input.Warrenty;
            bill.ServiceTime = input.ServiceTime;
            bill.Dealfa = input.Dealfa;
            bill.Faultap = input.Faultap;
           
            //处理图片
            PhotoProcess(input, bill, output);
            
            //完工
            _workOrderManager.Complete(bill);
            if (!output.Successed)
                throw new UserFriendlyException(output.Error);
            if (!string.IsNullOrEmpty(bill.YUNMsg))
            {
                //云办公已办
                string bootURL = ConfigurationManager.AppSettings["bootURL"];
                string pubKey = ConfigurationManager.AppSettings["pubKey"];
                string no = ConfigurationManager.AppSettings["no"];
                string pub = ConfigurationManager.AppSettings["pub"];
                string time = DateTime.Now.CurrentTimeMillis().ToString();
                string nonce = Guid.NewGuid().ToString();
                string appID = ConfigurationManager.AppSettings["appID"];//轻应用注册到云之家时生成
                string appSecret = ConfigurationManager.AppSettings["appSecret"];//轻应用注册到云之家时生成
                string yunxt = ConfigurationManager.AppSettings["yunxt"];
                YUNAPI.YUNXT = yunxt;
                string pubtoken = YUNAPI.CreatePubToken(no, pub, pubKey, nonce, time);
                var message = new
                {
                    no = no,
                    pub = pub,
                    time = time,
                    nonce = nonce,
                    pubtoken = pubtoken,
                    todoMsgIds = bill.YUNMsg,
                    todoStatus = YUNAPI.TODOSTAUS_DONE,
                    account = bill.AssignedPerson.UserName

                };
                try {
                    AsyncHelper.RunSync(() => YUNAPI.ChangeTodoMsgStatus(JsonConvert.SerializeObject(message)));
                }
                catch
                {

                }
            }
        }

        public void Report(ReportInput input)
        {
            var bill = _workOrderManager.Get(input.BillId);
            var archive = _equipmentArchiveAppService.GetArchiveBySerialNo(bill.SerialNo);
            if (bill.ServiceType.Equals(ServiceType.Install))
            {
                if (archive != null)
                    throw new UserFriendlyException("序列号已存在");
            }
            else
            {
                if (archive == null)
                    throw new UserFriendlyException("序列号不存在");
            }

            //记录修改回单活动
            if (bill.BillStatus == BillStatus.Dispatch)
            {
                var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
                var reportActivity = new Activity() { Bill = bill, Operater = user, Name = "修改回单", Log = string.Format("【{0}】修改了回单信息。", user.Name) };
                _workOrderManager.AddActivity(reportActivity);
            }
            //回单基本信息
            bill.SerialNo = input.SerialNo;
            bill.Office = input.Office;
            bill.OfficeMobile = input.OfficeMobile;
            bill.OfficePerson = input.OfficePerson;
            bill.OfficePosition = input.OfficePosition;
            bill.OfficeTel = input.OfficeTel;
            bill.Warrenty = input.Warrenty;
            bill.ServiceTime = input.ServiceTime;
            bill.Dealfa = input.Dealfa;
            
            bill.GuaranteedState = input.GuaranteedState;
            bill.TrafficLong = input.TrafficLong;
            bill.TrafficUrban = input.TrafficUrban;
            bill.Supply = input.Supply;
            bill.OtherEx = input.OtherEx;
            bill.HotelEx = input.HotelEx;

            //处理图片
            PhotoProcess(input, bill);

        }
        /// <summary>
        /// 回访
        /// </summary>
        /// <param name="input"></param>
        public void ReturnVisit(ReturnVisitInput input)
        {
            var workOrder = _workOrderManager.Get(input.BillId);
            var evalution = new ServiceEvalution()
            {
                Resolved = input.Resolved,
                ReturnVisitDesc = input.ReturnVisitDesc,
                Evaluation = input.Evaluation
            };

            //回访
            int evalId= _serviceEvalutionRepository.InsertAndGetId(evalution);
            workOrder.ServiceEvalution = evalution;
            ReturnVisit(workOrder);
        }
       
        /// <summary>
        /// 回访并关闭工单
        /// </summary>
        /// <param name="input"></param>
        public void ReturnVisitAndClose(ReturnVisitInput input)
        {
            var workOrder = _workOrderManager.Get(input.BillId);
            ReturnVisit(workOrder);
            Close(workOrder);
        }
        private void ReturnVisit(WorkOrderBill workOrder)
        {
            
            //记录回访活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var returnVisitActivity = new Activity() { Bill = workOrder, Operater = user,Log=string.Format("回访") };
            _workOrderManager.AddActivity(returnVisitActivity);

     
            _workOrderManager.ReturnVisit(workOrder);

        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="input"></param>
        public void Close(CloseInput input)
        {
            var workOrder = _workOrderManager.Get(input.BillId);
            Close(workOrder);
          
        }
        public void Close(int id)
        {
            var bill = _workOrderRepository.Get(id);
            Close(bill);
        }
        private void Close(WorkOrderBill bill)
        {
            _workOrderManager.Close(bill);
            //记录关闭活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var closeActivity = new Activity() { Bill = bill, Operater = user,Log=string.Format("关闭") };
            _workOrderManager.AddActivity(closeActivity);

        }
        public void Archive(int id)
        {
            var bill = _workOrderRepository.Get(id);
            if(string.IsNullOrEmpty( bill.SerialNo))
            {
                throw new UserFriendlyException("设备序列号不能为空");
            }
            var archive =_equipmentArchiveAppService.GetArchiveBySerialNo(bill.SerialNo);
            if(archive!=null)
            {
                var editInput = new EditEquipmentArchiveInput() {
                    AssignedPersonId = bill.ServiceType.Equals(ServiceType.Install) ? bill.AssignedPersonId : archive.AssignedPersonId,
                    CustomerId = bill.CustomerId,
                    InstallType = bill.ServiceType.Equals(ServiceType.Install) ? bill.InstallType : archive.InstallType,
                    Id=archive.Id,
                    Office=bill.Office,
                    OfficeMobile=bill.OfficeMobile,
                    OfficePerson=bill.OfficePerson,
                    OfficePosition=bill.OfficePosition,
                    OfficeTel=bill.OfficeTel,
                    ProductId=bill.ProductId,
                    SerialNo=bill.SerialNo,
                    ServiceTime= bill.ServiceType.Equals(ServiceType.Install) ? bill.ServiceTime : archive.ServiceTime,
                    Warrenty= bill.ServiceType.Equals(ServiceType.Install) ? bill.Warrenty : archive.Warrenty
                };
                _equipmentArchiveAppService.Edit(editInput);
            }
            else
            {
                var createInput = new CreateEquipmentArchiveInput()
                {
                    AssignedPersonId = bill.ServiceType.Equals(ServiceType.Install) ? bill.AssignedPersonId : new Nullable<long>(),
                    CustomerId = bill.CustomerId,
                    InstallType = bill.ServiceType.Equals(ServiceType.Install) ? bill.InstallType : "",
                    Office = bill.Office,
                    OfficeMobile = bill.OfficeMobile,
                    OfficePerson = bill.OfficePerson,
                    OfficePosition = bill.OfficePosition,
                    OfficeTel = bill.OfficeTel,
                    ProductId = bill.ProductId,
                    SerialNo = bill.SerialNo,
                    ServiceTime = bill.ServiceType.Equals(ServiceType.Install) ? bill.ServiceTime : new Nullable<DateTime>(),
                    Warrenty = bill.ServiceType.Equals(ServiceType.Install) ? bill.Warrenty : new Nullable<DateTime>()
                };
                _equipmentArchiveAppService.Create(createInput);
            }

            //记录归档活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var archiveActivity = new Activity() { Bill = bill, Operater = user, Log = string.Format("归档") };
            _workOrderManager.AddActivity(archiveActivity);

        }
        private void FaultEntryProcess(CompleteInput input,WorkOrderBill bill, CompleteOutput output)
        {
            if (input.Faults != null) {
                foreach (var fault in input.Faults)
                {
                    var fe = bill.FaultEntry.FirstOrDefault(x => x.FaultId == fault.FaultId);
                    if (fe == null)
                    {
                        //新增分录
                        fe = new WorkOrderFaultEntry();
                        fe.ParentId = bill.Id;
                        fe.FaultId = fault.FaultId;
                        //fe.FaultReasonId = fault.FaultReasonId;
                        //fe.FaultMeasureId = fault.FaultMeasureId;
                        _workOrderFaultEntryRepository.Insert(fe);

                    }

                }

            }
            for (int i = bill.FaultEntry.Count - 1; i >= 0; i--)
            {
                var entry = bill.FaultEntry.ElementAt(i);
                var entryInput = input.Faults.FirstOrDefault(x => x.FaultId == entry.FaultId);
                if (entryInput != null)
                {
                    //修改
                    entry.FaultId = entryInput.FaultId;
                    
                }
                else
                {
                    if (input.Faults == null)
                    {
                        //删除分录
                        _workOrderFaultEntryRepository.Delete(entry.Id);
                        bill.FaultEntry.Remove(entry);
                        i--;

                    }
                    else {
                        if (entryInput==null) {
                            //删除分录
                            _workOrderFaultEntryRepository.Delete(entry.Id);
                            bill.FaultEntry.Remove(entry);
                            i--;
                            
                        }

                    }
                      

                   
                   
                    
                }
            }

           
        }
        private void PhotoProcess(ReportInput input, WorkOrderBill bill)
        {
           
            for (int i = bill.Photos.Count - 1; i >= 0; i--)
            {
                WorkOrderPhoto photo = bill.Photos.ElementAt(i);
                //删除分录
                _workOrderPhotoRepository.Delete(photo);
               // i--;
            }
            if (input.Photos == null) return;
            foreach (var photo in input.Photos)
            {
                var newPhoto=new WorkOrderPhoto()
                {
                    FilePath = photo,
                    Bill=bill
                };
                _workOrderPhotoRepository.Insert(newPhoto);
            }
                
        }
        private void PhotoProcess(CompleteInput input, WorkOrderBill bill, CompleteOutput output)
        {
            if (input.Photos != null)
            {
                foreach (var photo in input.Photos)
                {
                    var newPhoto = bill.Photos.FirstOrDefault(x => x.FilePath == photo.FilePath);
                    if (newPhoto == null)
                    {
                        //新增分录
                        newPhoto = new WorkOrderPhoto();
                        newPhoto.Bill = bill;
                        newPhoto.FilePath = photo.FilePath;
                        _workOrderPhotoRepository.Insert(newPhoto);
                    }

                }
            }
            //删除照片
            for (int i = bill.Photos.Count - 1; i >= 0; i--)
            {
               WorkOrderPhoto photo = bill.Photos.ElementAt(i);
                
               if (input.Photos == null)
                {
                    //删除分录
                    _workOrderPhotoRepository.Delete(photo);
                    i--;
                }
                else
                {
                    var photoInput = input.Photos.FirstOrDefault(x => x.FilePath == photo.FilePath);
                    if(photoInput==null)
                    {
                        //删除分录
                        _workOrderPhotoRepository.Delete(photo);
                        i--;
                    }

                }
               
            }

            

        }
        private void AccessoryEntryProcess(CompleteInput input,WorkOrderBill bill, CompleteOutput output)
        {
            if (input.Accessories != null) {

                //新增
                foreach (var accessory in input.Accessories)
                {
                    var entry = bill.AccessoryEntry.FirstOrDefault(x => x.AccessoryId == accessory.AccessoryId);
                    if (entry == null)
                    {
                        entry = new WorkOrderAccessoryEntry();
                        entry.AccessoryId = accessory.AccessoryId;
                        entry.Count = accessory.Count;
                        entry.NewSerials = entry.OldSerials;
                        entry.BillId = bill.Id;
                        Accessory accessoryEntity = _accessoryRepository.Get(accessory.AccessoryId);
                        _workOrderAccessoryEntryRepository.Insert(entry);
                        int currentCount = _inventoryDomainService.GetAccessoryCountByUser(bill.AssignedPerson, accessoryEntity);
                        if (currentCount >= entry.Count)
                        {
                            //扣减库存
                            _inventoryDomainService.UpdateStaffInventory(bill.AssignedPerson, entry.Accessory, entry.Count * (-1));
                        }
                        else
                        {
                            //库存不足
                            output.Successed = false;
                            string msg = string.Format("配件【{0}】持有量不足，缺少{1}{2}\r\n", entry.Accessory.Name, entry.Count - currentCount, entry.Accessory.Unit);
                            output.Error = output.Error + msg;
                        }

                    }
                }


            }
            //删除分录
            for (int i = bill.AccessoryEntry.Count - 1; i >= 0; i--)
            {
                WorkOrderAccessoryEntry accessoryEntry = bill.AccessoryEntry.ElementAt(i);
               

                if (input.Accessories == null)
                {

                    //增加配件持有数量
                    _inventoryDomainService.UpdateStaffInventory(bill.AssignedPerson, accessoryEntry.Accessory, accessoryEntry.Count);
                    //删除分录
                    _workOrderAccessoryEntryRepository.Delete(accessoryEntry);
                   
                   
                    i--;
                }
                else {
                    var entryInput = input.Accessories.FirstOrDefault(x => x.AccessoryId == accessoryEntry.AccessoryId);
                    if (entryInput == null)
                    {
                        //增加配件持有数量
                        _inventoryDomainService.UpdateStaffInventory(bill.AssignedPerson, accessoryEntry.Accessory, accessoryEntry.Count);
                        //删除分录
                        _workOrderAccessoryEntryRepository.Delete(accessoryEntry);
                       
                       
                        i--;
                    }
                    else
                    {
                        
                        //增加配件持有数量
                        _inventoryDomainService.UpdateStaffInventory(bill.AssignedPerson, accessoryEntry.Accessory, accessoryEntry.Count);

                        //检查库存量
                        int currentCount = _inventoryDomainService.GetAccessoryCountByUser(bill.AssignedPerson, accessoryEntry.Accessory);
                        if (currentCount >= entryInput.Count)
                        {
                            //更新分录
                            accessoryEntry.Count = entryInput.Count;
                            accessoryEntry.NewSerials = entryInput.NewSerials;
                            accessoryEntry.OldSerials = entryInput.OldSerials;
                            //扣减
                            _inventoryDomainService.UpdateStaffInventory(bill.AssignedPerson, accessoryEntry.Accessory, accessoryEntry.Count * (-1));
                        }
                        else
                        {
                            //库存不足
                            output.Successed = false;
                            string msg = string.Format("配件【{0}】持有量不足，缺少{1} {2}\r\n", accessoryEntry.Accessory.Name, accessoryEntry.Count - currentCount, accessoryEntry.Accessory.Unit);
                            output.Error = output.Error + msg;
                        }
                        //i--;

                    }
                }

               
            }
           
                  
            

        }
        /// <summary>
        /// 取得工单详细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public GetDetailOutput GetDetail(GetDetailInput input)
        {
            //有管理全部工单的权限，能查看所有工单，否则只能查看分配给自己的工单

            var query = _workOrderRepository
             .GetAllIncluding()
             .Where(e => e.Id == input.Id);

            //if (!PermissionChecker.IsGranted("WorkOrder.ManageAllWorkOrder"))
            //{
            //    query.Where(e => e.AssignedPersonId == AbpSession.UserId);
            //}

            var workOrder=  query.FirstOrDefault();
            if (workOrder == null)
            {
                 throw new UserFriendlyException("工单不存在");
            }
            return workOrder.MapTo<GetDetailOutput>();
        }

        /// <summary>
        /// 返回工单列表 非管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<WorkOrderListDto> GetStaffList(GetListInput input)
        {
            var query = _workOrderRepository
              .GetAll()
                    //接单人过滤
                    .Where(e => e.AssignedPersonId == AbpSession.UserId)
                    //搜索条件
                    .WhereIf(!string.IsNullOrEmpty(input.KeyWord),
                        e => (e.Customer.Name.Contains(input.KeyWord)
                        || e.Product.Name.Contains(input.KeyWord)
                        || e.Number.Contains(input.KeyWord)
                        )
                        )
                    //待处理
                    .WhereIf(input.QueryType.Equals(WorkOrderListType.Undo),
                        e => e.BillStatus == BillStatus.Dispatch
                        //|| e.BillStatus == BillStatus.Complete
                        )
                    //已处理
                    .WhereIf(input.QueryType.Equals(WorkOrderListType.Done), 
                        e => e.BillStatus == BillStatus.Complete
                        || e.BillStatus==BillStatus.ReturnVisit
                        || e.BillStatus==BillStatus.Close);

            var recordCount = query.Count();
            var list = query.OrderByDescending(e => e.DispatchTime)
                .PageBy(input)
                .ToList();
            var billList = list.MapTo<List<WorkOrderListDto>>();
            return new PagedResultDto<WorkOrderListDto>(recordCount, billList);

        }

        /// <summary>
        /// 新建或受理工单
        /// </summary>
        /// <param name="input"></param>
        public void CreateAndAcceptWorkOrder(CreateOrUpdateWorkOrderInput input)
        {
            var workOrder=Create(input);
            Accept(workOrder);
        }

        

        /// <summary>
        /// 新建或编辑工单
        /// </summary>
        /// <param name="input"></param>
        public void CreateOrUpdateWorkOrder(CreateOrUpdateWorkOrderInput input)
        {

            if (input.Id.HasValue)
            {
                Update(input);
                
            }
            else
            {
                Create(input);
            }

           
        }

        
        public GetWorkOrderForEditOutput GetWorkOrderForEdit(int id)
        {
            var workorder = _workOrderRepository.Get(id);
           
            return workorder.MapTo<GetWorkOrderForEditOutput>();
        }
        
        

        /// <summary>
        /// 新建工单
        /// </summary>
        /// <param name="input"></param>
        public WorkOrderBill Create(CreateOrUpdateWorkOrderInput input)
        {
            //产品
            var product = _productRepository.Get(input.ProductId);
            //新建或编辑客户
            //Customer customer = CreateOrUpdateCustomer(input);
            var customer = _customerDomainService.Get(input.CustomerId);

            //新建工单
            var workOrder = input.MapTo<WorkOrderBill>();
            workOrder.Customer = customer;
            workOrder.CustomerName = customer.Name;
            workOrder.CustomerCode = customer.Code;
            workOrder.CustomerArea = input.CustomerArea;

            workOrder.Product = product;
            //workOrder.ProductName = product.Name;
            //workOrder.ProductMode = product.Model;

            if (input.Description == null)
            {
                workOrder.Description = "";
            }
            else {

                workOrder.Description = input.Description.Trim();
            }


            _workOrderManager.Create(workOrder);

            //记录新建活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var createActivity = new Activity()
            {
                Bill = workOrder,
                Operater = user,
                Name = "新建",
                Log = string.Format("【{0}】 为客户【{1}】创建了【{2}】工单。",
                    user.Name, workOrder.Customer.Name, workOrder.ServiceType.Equals(ServiceType.Install) ? "安装" : "维修")
            };
            _workOrderManager.AddActivity(createActivity);
            return workOrder;

        }
        /// <summary>
        /// 编辑工单
        /// </summary>
        /// <param name="input"></param>
        private WorkOrderBill Update(CreateOrUpdateWorkOrderInput input)
        { //客户
            var customer = _customerDomainService.Get(input.CustomerId);
            //产品
            var product = _productRepository.Get(input.ProductId);
            //编辑工单
            var workOrder = _workOrderManager.Get(input.Id.Value);
            string msg = "";
            if (workOrder.CustomerId != input.CustomerId)
                msg += "客户";
            if (workOrder.CustomerId == input.CustomerId)
            {
                if (workOrder.CustomerPhone != input.CustomerPhone)
                    msg += " 客户电话";
                if (workOrder.CustomerArea != input.CustomerArea)
                    msg += " 客户区域";
                if (workOrder.Address != input.Address)
                    msg += " 详细地址";
                if (workOrder.ProductId != input.ProductId)
                    msg += " 产品";
                if (workOrder.SaleMan != input.SaleMan)
                    msg += " 业务员";
                if (workOrder.SaleManPhone != input.SaleManPhone)
                    msg += " 业务员电话";
                if (workOrder.ServiceType != input.ServiceType)
                    msg += " 工单类型";
                if (workOrder.Priority != input.Priority)
                    msg += " 优先级";
                if (workOrder.Description != input.Description)
                    msg += " 服务描述";
                if (workOrder.SaleOrg != input.SaleOrg)
                    msg += " 销售组织";
                if (workOrder.IssueBill != input.IssueBill)
                    msg += " 出库单号";
            }

            input.MapTo(workOrder);

            workOrder.Customer = customer;
            workOrder.CustomerName = customer.Name;
            workOrder.CustomerCode = customer.Code;
            workOrder.Product = product;
            //workOrder.ProductName = product.Name;
            //workOrder.ProductMode = product.Model;

            if (workOrder.Description == null)
            {
                workOrder.Description = "";
            }
            else {
                workOrder.Description = input.Description.Trim();
            }
            
            //记录编辑活动
            var user = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync(UserManager.AbpSession.UserId.Value));
            var createActivity = new Activity()
            {
                Bill = workOrder,
                Operater = user,
                Name = "编辑",
                Log = string.Format("修改了工单的{0}信息", msg)
            };
            _workOrderManager.AddActivity(createActivity);

            return workOrder;

        }

        /// <summary>
        /// 工单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<WorkOrderListDto> GetWorkOrders(GetWorkOrderInput input)
        {

            var query = _workOrderRepository.GetAll()
            .WhereIf(!string.IsNullOrEmpty(input.Where.SearchKey),
                e => e.Customer.Name.Contains(input.Where.SearchKey)
                || e.Id.ToString().Equals(input.Where.SearchKey))
            .WhereIf(!string.IsNullOrEmpty(input.Where.SerialNo), e => e.SerialNo.Equals(input.Where.SerialNo));

            // query.Where(e => e.BillStatus == BillStatus.Complete);
            if (input.Where.Filter.HasValue)
            {
                if (input.Where.Filter == 2)
                {
                    query = query.Where(e => e.BillStatus != BillStatus.Close)
                      .Where(e => e.BillStatus != BillStatus.Cancel);
                }
                else if (input.Where.Filter == 3)
                {
                    query = query.Where(e => DbFunctions.DiffDays(e.CreationTime, DateTime.Now) == 0);
                }
                else if (input.Where.Filter == 4)
                {
                    query = query.Where(e => e.BillStatus == BillStatus.Complete);

                }
                else if (input.Where.Filter == 5)
                {
                    query = query.Where(e => (e.BillStatus == BillStatus.Save || e.BillStatus == BillStatus.Accept));
                }

            }
            var orderCount = query.Count();
            var workOrders = query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();

            var workOrderListDto = workOrders.MapTo<List<WorkOrderListDto>>();
            return new PagedResultDto<WorkOrderListDto>(
                orderCount,
                workOrderListDto
                );
        }

      /// <summary>
      /// 人员配件使用记录 
      /// </summary>
      /// <param name="input"></param>
      /// <returns></returns>
       public PagedResultDto<GetNewStaffUseDto> GetNewStaffUse(GetNewStaffUseInput input)
       {
            var query = _workOrderRepository
                .GetAll()
                .Include(t=>t.AccessoryEntry)
                .Where(t => t.AssignedPersonId == input.StaffID);
            var recordCount = query.Count();
            var list = query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();
            var billList = list.MapTo<List<GetNewStaffUseDto>>();
            return new PagedResultDto<GetNewStaffUseDto>(recordCount, billList);
        }

        /// <summary>
        /// 配件使用明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<GetNewAccessoryUseDto> GetNewAccessoryUse(GetNewAccessoryUseInput input)
        {
            //var query = _workOrderRepository
            //    .GetAll()
            //    .Include(t => t.AccessoryEntry)
            //    .WhereIf(input.Where.StaffID.HasValue, t => t.AssignedPersonId == input.Where.StaffID.Value)
            //    .WhereIf(input.Where.StartDate.HasValue,t=>t.CreationTime>=input.Where.StartDate.Value)
            //    .WhereIf(input.Where.EndDate.HasValue,t=>t.CreationTime<=input.Where.EndDate.Value)
            //    .Where(t=>t.AccessoryEntry.Count>0)
            //    ;
           var query = from entry in _workOrderAccessoryEntryRepository.GetAllIncluding()
            join workOrder in _workOrderRepository.GetAllIncluding() on entry.BillId  equals workOrder.Id
            select new GetNewAccessoryUseDto()
            {
                AccessoryId =entry.AccessoryId,
                AccessoryName = entry.Accessory.Name,
                AccessoryNumber= entry.Accessory.Number,
                AccessoryModel =entry.Accessory.Model,
                AccessoryUnit = entry.Accessory.Unit,
                Count =entry.Count,
                BackCount =entry.BackCount ,
                CreationTime = workOrder.CreationTime,
                StaffID=workOrder.AssignedPersonId.Value
            };
            query
                .WhereIf(input.Where.StaffID.HasValue, t => t.StaffID == input.Where.StaffID.Value)
                .WhereIf(input.Where.StartDate.HasValue, t => t.CreationTime >= input.Where.StartDate.Value)
                .WhereIf(input.Where.EndDate.HasValue, t => t.CreationTime <= input.Where.EndDate.Value);
            
            var list = query.OrderBy(input.Sorting)
                .PageBy(input)
                .ToList();
            var recordCount = list.Count;
           // var billList = list.MapTo<List<GetNewAccessoryUseDto>>();
            return new PagedResultDto<GetNewAccessoryUseDto>(recordCount, list);
        }

        /// <summary>
        /// 所有工单数量
        /// </summary>
        /// <returns></returns>
        public int GetAllCount()
        {
           return _workOrderRepository.Count();
        }
        /// <summary>
        /// 某日新增工单数量
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public int GetCountByCreationTime(DateTime datetime)
        {
           return _workOrderRepository
                .GetAll()
                .Where(e => DbFunctions.DiffDays(e.CreationTime,datetime)==0)
                .Count();
        }
        /// <summary>
        /// 未派工数量
        /// </summary>
        /// <returns></returns>
        public int GetUnDispatchCount()
        {
            var query = _workOrderRepository
                .GetAll()
                .Where(e => (e.BillStatus==BillStatus.Save || e.BillStatus==BillStatus.Accept));
            return query.Count();
        }
        /// <summary>
        /// 待回访数量
        /// </summary>
        /// <returns></returns>
        public int GetUnReturnVisitCount()
        {
            var query = _workOrderRepository
               .GetAll()
               .Where(e => e.BillStatus==BillStatus.Complete);
            return query.Count();
        }
        /// <summary>
        /// 未解决数量
        /// </summary>
        /// <returns></returns>
        public int GetUnCloseCount()
        {
            var query = _workOrderRepository
               .GetAll()
               .Where(e => e.BillStatus!=BillStatus.Close)
               .Where(e=>e.BillStatus != BillStatus.Cancel);
            return query.Count();

        }

        /// <summary> 
        /// 返回我申请的工单 
        /// </summary> 
        /// <param name="input"></param> 
        /// <returns></returns> 
        public PagedResultDto<WorkOrderListDto> GetStaffCreate(GetStaffCreateInput input)
        {
            var query = _workOrderRepository
            .GetAll()
            .Where(e => e.CreatorUserId == AbpSession.UserId)
            //搜索条件 
            .WhereIf(!string.IsNullOrEmpty(input.KeyWord),
                e => (e.Customer.Name.Contains(input.KeyWord)
                || e.Product.Name.Contains(input.KeyWord)
                || e.Number.Contains(input.KeyWord)
                )
            );
            var recordCount = query.Count();
            var list = query.OrderByDescending(e => e.CreationTime)
            .PageBy(input)
            .ToList();
            var billList = list.MapTo<List<WorkOrderListDto>>();
            return new PagedResultDto<WorkOrderListDto>(recordCount, billList);
        }


        /// <summary> 
        /// 科室历史记录 
        /// </summary> 
        /// <param name="customerId"></param> 
        /// <returns></returns> 
        public ListResultDto<string> GetCustomerOffices(string customerId)
        {
            var offices = _workOrderRepository.GetAll()
            .Where(x => x.CustomerId == customerId)
            .Where(x => x.Office != null)
            .Select(x => x.Office)
            .Distinct();
            return new ListResultDto<string>(offices.ToList());

        }
        /// <summary>
        /// 获取差旅费用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetExpenseOutput GetExpense(int id)
        {
            var workOrder = _workOrderRepository.Get(id);
            if (workOrder == null)
            {
                throw new UserFriendlyException("工单不存在");
            }
            return workOrder.MapTo<GetExpenseOutput>();
        }
    }
}
