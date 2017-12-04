using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAMS.Web.Areas.StaffMobile.Models;
using SAMS.Web.Controllers;
using SAMS.WorkOrders;
using SAMS.WorkOrders.Dtos;
using System.Threading.Tasks;
using SAMS.Web.Areas.StaffMobile.Models.WorkOrder;
using SAMS.Inventory;
using Microsoft.AspNet.Identity;
using SAMS.Users;
using SAMS.Faults;
using Abp.UI;
using Abp.Web.Mvc.Authorization;

using System.IO;
using SAMS.Customers;
using SAMS.Products;

namespace SAMS.Web.Areas.StaffMobile.Controllers
{
    [AbpMvcAuthorize(Roles = "Staff")]
    public class HomeController : SAMSControllerBase
    {
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IFaultAppService _faultAppSercice;
        private readonly UserManager _userManager;
        private readonly ICustomerAppService _customerAppService;
        private readonly IProductAppService _productAppService;

        public HomeController(IWorkOrderAppService workOrderAppService, IInventoryAppService inventoryAppService, UserManager userManager, IFaultAppService faultAppSercice, ICustomerAppService customerAppService, IProductAppService productAppService)
        {
            _workOrderAppService = workOrderAppService;
            _inventoryAppService = inventoryAppService;
            _faultAppSercice = faultAppSercice;
            _userManager = userManager;
            _customerAppService = customerAppService;
            _productAppService = productAppService;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AskOrder()
        {
            var input = new GetStaffCreateInput() { MaxResultCount = 20000,  SkipCount = 0 };
            var askOrderList = _workOrderAppService.GetStaffCreate(input);
            var viewModel = new AskOrderViewModel() { WorkOrders = askOrderList };
            return View(viewModel);
        }

        public ActionResult CreateOrder()
        {
           
            var products = _productAppService.GetProducts();
            var viewModel = new CreateOrderViewModel() { Products = products};
            return View(viewModel);
        }

        public ActionResult CommitOrder(int id)
        {
            var input = new GetDetailInput() { Id = id };
            var workOrder = _workOrderAppService.GetDetail(input);

            var products = _productAppService.GetProducts();
            var viewModel = new CommitOrderViewModel() { Products = products ,WorkOrder=workOrder};
            return View(viewModel);
        }

        public ActionResult UndoOrder()
        {
            var input = new GetListInput() { MaxResultCount = 20000, QueryType = WorkOrderListType.Undo, SkipCount = 0 };
            
            var workOrderList =  _workOrderAppService.GetStaffList(input);
            var viewModel = new UndoOrderViewModel() { WorkOrders = workOrderList };
            
            return View(viewModel);
        }
        public ActionResult Detail(int id)
        {
            var input = new GetDetailInput() { Id = id };
            var workOrder = _workOrderAppService.GetDetail(input);
            var viewModel = new DetailViewModel() { WorkOrder = workOrder };
            return View(viewModel);
        }
        public ActionResult Accessory()
        {
            var myAccessories =_inventoryAppService.GetStaffStockList(_userManager.AbpSession.UserId.Value);
            var viewmodel = new AccessoryViewModel() { MyAccessories = myAccessories };
            return View(viewmodel);
        }

        public ActionResult DoneOrder()
        {
            var input = new GetListInput() { MaxResultCount = 20000, QueryType = WorkOrderListType.Done, SkipCount = 0 };

            var workOrderList = _workOrderAppService.GetStaffList(input);
            var viewModel = new DoneOrderViewModel() { WorkOrders = workOrderList };

            return View(viewModel);
        }
        
       
        //回单
        public ActionResult Receipt(int id)
        {
            var input = new GetDetailInput() { Id = id };
            var workOrder = _workOrderAppService.GetDetail(input);

            var myAccessories = _inventoryAppService.GetStaffStockListByProduct(_userManager.AbpSession.UserId.Value,workOrder.ProductId);
            var faults =_faultAppSercice.GetFaults();

            var model = new ReportModel() { WorkOrder = workOrder ,MyAccessories= myAccessories,Faults=faults };


            return View(model);
        }

        public ActionResult Expense(int id) {

            var input = new GetDetailInput() { Id = id };

            var workOrder = _workOrderAppService.GetDetail(input);
            var model = new ExpenseModel() { WorkOrder = workOrder };

            return View(model);
             
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SavePhoto(HttpPostedFileBase uploadfile)
        {
            string fileName = "";
            if (uploadfile != null)
            {
                if (uploadfile.ContentLength > 0)
                {
                    fileName = Path.GetFileName(uploadfile.FileName);
                    fileName = Guid.NewGuid() + "_" +  fileName;
                    string path = Path.Combine(Server.MapPath("~/upload/"), fileName);
                    uploadfile.SaveAs(path);
                }
            }
            var result = new { FileName=fileName,fileType= uploadfile.ContentType };
            return Json(result);
        }
        /// <summary>
        /// 保存回单信息
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult Receipt(Receipt receipt)
        {
            var input = new CompleteInput() {
                BillId = receipt.BillId,
                SerialNo = receipt.SerialNo,
                GuaranteedState = receipt.GuaranteedState,
                Office = receipt.Office,
                OfficeMobile = receipt.OfficeMobile,
                OfficePerson = receipt.OfficePerson,
                OfficePosition = receipt.OfficePosition,
                OfficeTel = receipt.OfficeTel,
                ServiceTime = receipt.ServiceTime,
                Warrenty = receipt.Warrenty,
                //CustomerPhone = receipt.CustomPhone,
                Accessories = receipt.Accessories == null ? null : receipt.Accessories.Select(u => new WorkOrderAccessoryInputDto
                {
                    //产生二义性
                    //AccessoryId = u.AccessoryId,
                    //Count = u.Count,
                    NewSerials = u.NewSerial,
                    OldSerials = u.OldSerial

                }),
                Faults = receipt.Faults == null ? null : receipt.Faults.Select(u => new WorkOrderFaultInputDto
                {
                    FaultId = u.FaultId
                }),
                Photos = receipt.Photos == null?null:receipt.Photos.Select(p=> new WorkOrderPhotoDto
                {
                    BillId =receipt.BillId,
                    FilePath=p.FileName,
                    FileType=p.FileType
                   
                }),
                //todo:添加字段
                Faultap =receipt.Faultap,
                Dealfa=receipt.Dealfa
                //TrafficUrban = receipt.TrafficUrban,
                //TrafficLong = receipt.TrafficLong,
                //HotelEx = receipt.HotelEx,
                //Supply = receipt.Supply,
                //OtherEx = receipt.OtherEx

            };
             _workOrderAppService.Complete(input); 
       
            
            return Content("ok");

        }
        [HttpPost]
        public ContentResult Expense(Receipt receipt) {

            var input = new ExpenseInput()
            {
                BillId=receipt.BillId,
                TrafficUrban=receipt.TrafficUrban,
                TrafficLong=receipt.TrafficLong,
                HotelEx=receipt.HotelEx,
                Supply=receipt.Supply,
                OtherEx=receipt.OtherEx


            };
            _workOrderAppService.UpdateExpense(input);
            return Content("ok");

        }


        /// <summary> 
        /// 根据区域选择客户 
        /// </summary> 
        /// <param name="location"></param> 
        /// <returns></returns> 
        //[HttpPost]
        //public JsonResult GetCustomer(string location)
        //{
        //    var customers = _customerAppService.GetCustomerByLocation(location);
        //    return Json(customers);
        //}
        /// <summary> 
        /// 工单申请 
        /// </summary> 
        /// <param name="workOrder"></param> 
        /// <returns></returns> 
        [HttpPost]
        public ContentResult CreateOrUpdate(CreateOrUpdateOrder workOrder)
        {
            var input = new CreateOrUpdateWorkOrderInput()
            {
              
                    CustomerId = workOrder.CustomerId,
                    Description = workOrder.Description,
                    Id = workOrder.Id,
                    Office = workOrder.Office,
                    OfficeMobile = workOrder.OfficeMobile,
                    OfficePerson = workOrder.OfficePerson,
                    //OfficePosition = workOrder.OfficePosition,
                    OfficeTel = workOrder.OfficeTel,
                    ServiceType = workOrder.ServiceType,
                    ProductId = workOrder.ProductId
                    
                   
                
            };

            _workOrderAppService.CreateOrUpdateWorkOrder(input);
            return Content("ok");
        }


        /// <summary>
        /// 科室历史记录 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCustomerOffices(string customerId) {
            var customerOffices = _workOrderAppService.GetCustomerOffices(customerId);
            return Json(customerOffices);

        }
    }
}