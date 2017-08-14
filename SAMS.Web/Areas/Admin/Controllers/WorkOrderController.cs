using Abp.Web.Mvc.Authorization;
using SAMS.Products;
using SAMS.Products.Dtos;
using SAMS.Web.Areas.Admin.Models.WorkOrder;
using SAMS.Web.Controllers;
using SAMS.WorkOrders;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SAMS.Authorization;
using SAMS.Users;
using Abp.Extensions;
using Abp.Linq.Extensions;
using SAMS.Inventory;
using System.Web.Script.Serialization;
using SAMS.Web.Areas.Admin.Models.Common;
using Newtonsoft.Json;
using SAMS.Customers;

namespace SAMS.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize(Roles = "Admin")]
    public class WorkOrderController : SAMSControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly UserManager _userManager;
        private readonly IInventoryAppService _inventoryAppService;
        private readonly ICustomerAppService _customerAppService;

     

        public WorkOrderController(IProductAppService productAppService, IInventoryAppService inventoryAppService, 
            IWorkOrderAppService workOrderAppService, UserManager userManager,ICustomerAppService customerAppService)
        {
            _productAppService = productAppService;
            _workOrderAppService = workOrderAppService;
            _userManager = userManager;
            _inventoryAppService = inventoryAppService;
            _customerAppService = customerAppService;
        }
        // GET: Admin/WorkOrders
        public ActionResult Index()
        {
            ViewBag.ID = 1;
            return View("Index");
        }
        /// <summary>
        /// 工单编号 客户名称 查找工单
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public ActionResult Search(String number)
        {
            ViewData["Number"] = number;
            return View();
        }
        // [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public PartialViewResult CreateOrEditModal(int? id)
        {
            var viewModel = new CreateOrEditWorkOrderModalViewModel();

            var products = _productAppService.GetProducts();
            viewModel.Products = products.Items.ToArray<ProductListDto>();

            if (id.HasValue)
            {
                var output = _workOrderAppService.GetWorkOrderForEdit(id.Value);
                viewModel.WorkOrder = output.WorkOrder;
            }
             
            return PartialView("_CreateOrEditModal", viewModel);
        }

        public PartialViewResult PopCreate(PopCreateParam param)
        {
            var popCreateViewModel = new PopCreateViewModel();
            var products = _productAppService.GetProducts();
            popCreateViewModel.Products = products.Items.ToArray<ProductListDto>();
            popCreateViewModel.CustomerId = param.Id;
            popCreateViewModel.CustomerName = param.Name;
            popCreateViewModel.CustomerPhone = param.Mobile;
            return PartialView("_PopCreate", popCreateViewModel);
        }
        public ActionResult Views(int id)
        {
            ViewBag.ID = id;
            return View("index");
        }
        public PartialViewResult ListView(int id)
        {
            ViewBag.ActiveID = id;
            //所有工单
            ViewBag.View1= _workOrderAppService.GetAllCount();
            //未解决工单
            ViewBag.View2= _workOrderAppService.GetUnCloseCount();
            //今日新增工单
            ViewBag.View3 = _workOrderAppService.GetCountByCreationTime(DateTime.Now);
            //待回访工单
            ViewBag.View4 = _workOrderAppService.GetUnReturnVisitCount();
            //待派工工单
            ViewBag.View5 = _workOrderAppService.GetUnDispatchCount();

            return PartialView("_ListView");
        }
        public PartialViewResult PopCreateWorkOrderCustomer()
        {
            var popCreateViewModel = new PopCreateViewModel();
            var products = _productAppService.GetProducts();
            popCreateViewModel.Products = products.Items.ToArray<ProductListDto>();
            return PartialView("_PopCreateWorkOrderCustomer", popCreateViewModel);
        }
        public PartialViewResult PopEdit(int id)
        {
            var viewModel = new PopEditViewModel();
            var products = _productAppService.GetProducts();
            viewModel.Products = products.Items.ToArray<ProductListDto>();
            viewModel.WorkOrder = _workOrderAppService.GetWorkOrderForEdit(id).WorkOrder;
            return PartialView("_PopEdit", viewModel);

        }
        [HttpPost]
        public ActionResult PopCreateWorkOrderCustomer(PopCreateWorkOrderCustomerInput viewInput)
        {
            
            var input = new CreateOrUpdateWorkOrderInput()
            {
                WorkOrder = new WorkOrderEditDto()
                {
                    Address = viewInput.Customer.Address,
                   
                    CustomerName = viewInput.Customer.Name,
                    CustomerId = viewInput.Customer.Id,
                    
                    CustomerArea = viewInput.Customer.Area,
                    Description = viewInput.WorkOrder.Description,
                    
                    ProductId = viewInput.WorkOrder.ProductId,
                    SaleMan = viewInput.WorkOrder.SaleMan,
                    SaleManPhone = viewInput.WorkOrder.SaleManPhone,
                    ServiceType = viewInput.WorkOrder.ServiceTypeID,

                    Office=viewInput.Customer.Office,
                    OfficePerson=viewInput.Customer.OfficePerson,
                    OfficePosition=viewInput.Customer.OfficePosition,
                    OfficeMobile=viewInput.Customer.OfficeMobile,
                    SerialNo=viewInput.WorkOrder.SerialNo
                    

                }
            };
            _workOrderAppService.CreateAndAcceptWorkOrder(input);
            return Content("ok");
        }
        public ActionResult PopEditWorkOrder(PopEditWorkOrderViewModel model)
        {
            CreateOrUpdateWorkOrderInput input = new CreateOrUpdateWorkOrderInput()
            {
                WorkOrder = new WorkOrderEditDto()
                {
                    Address = model.Customer.Address,
                    
                    CustomerName = model.Customer.Name,
                    CustomerId = model.Customer.Id,
                    
                    CustomerArea = model.Customer.Area,
                    Description = model.WorkOrder.Description,
                   
                    ProductId = model.WorkOrder.ProductId,
                    SaleMan = model.WorkOrder.SaleMan,
                    SaleManPhone = model.WorkOrder.SaleManPhone,
                    ServiceType = model.WorkOrder.ServiceTypeID,
                    Id = model.WorkOrder.Id

                }
            };
            _workOrderAppService.CreateOrUpdateWorkOrder(input);
            return Content("ok");
        }
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            var customerInput = new Customers.Dtos.GetDetailInput() { Id = model.CustomerID };
            var customer = _customerAppService.GetDetail(customerInput);
            var input = new CreateOrUpdateWorkOrderInput() {
                WorkOrder = new WorkOrderEditDto() {
                    Address = customer.Address ,
                   
                    CustomerName=customer.Name,
                    CustomerId=customer.Id,
                    
                    CustomerArea=customer.Area,
                    Description=model.Description,
                    
                    ProductId=model.ProductID,
                    SaleMan=model.SaleMan,
                    SaleManPhone=model.SaleManPhone,
                    ServiceType=model.ServiceTypeID
                }
            };
            _workOrderAppService.CreateAndAcceptWorkOrder(input);
            return Content("ok");
        }
        
        public PartialViewResult GetList(GetListParam param)
        {
            int skipCount = param.PageSize*(param.PageIndex-1);
            string sorting = null;
            if(!string.IsNullOrEmpty(param.Sort))
                sorting=param.Sort +" "+param.Direction;
            var viewModel = new GetListViewModel();
            var where = JsonConvert.DeserializeObject<WhereParam>(param.Where);
          
            GetWorkOrderInput input = new GetWorkOrderInput() {MaxResultCount=param.PageSize,SkipCount=skipCount,Sorting=sorting,Where=where};
            
            viewModel.WorkOrders = _workOrderAppService.GetWorkOrders(input);
            
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            
            
            return PartialView("_GetList",viewModel);
        }
        public ActionResult Details(int id)
        {
            TempData["tempId"] = id;
            var input = new SAMS.WorkOrders.Dtos.GetDetailInput() { Id = id };
            ViewBag.WorkOrder = _workOrderAppService.GetDetail(input);
            return View();
        }
        //受理
        public ContentResult Accept(int id)
        {

            _workOrderAppService.Accept(id);
            return Content("ok");
        }
        //取消
        public ContentResult Cancel(int id)
        {

            _workOrderAppService.Cancel(id);
            return Content("ok");
        }
        //派工
        [HttpGet]
        public PartialViewResult Dispatch(int id)
        {
            var input = new SAMS.WorkOrders.Dtos.GetDetailInput() { Id = id };
            var workOrder = _workOrderAppService.GetDetail(input);

            var users = _userManager.Users.ToList();
            var userList=new SelectList(users, "ID", "Name", workOrder.AssignedPersonId.HasValue? workOrder.AssignedPersonId.Value: users.First().Id);
            ViewBag.Id = id;
            ViewBag.UserList = userList;
            ViewBag.NextHandlerName = workOrder.AssignedPersonId.HasValue ? workOrder.AssignedPersonName : users.First().Name;
            ViewBag.NextHandlerID= workOrder.AssignedPersonId.HasValue ? workOrder.AssignedPersonId.Value : users.First().Id;
            ViewBag.Area = workOrder.CustomerArea;
            ViewBag.CustomerName = workOrder.CustomerName;

            return PartialView("_Dispatch");

        }
        public PartialViewResult ReturnVisit(int id)
        {
            ViewBag.Id = id;
            return PartialView("_ReturnVisit");
        }

        
        //回单
        public PartialViewResult Report(int id)
        {
            var input = new SAMS.WorkOrders.Dtos.GetDetailInput() { Id = id };
            var workOrder = _workOrderAppService.GetDetail(input);
            var model = new ReportModel() { WorkOrder = workOrder };
           
            return PartialView("_Report",model);
        }
        //选择配件
        public PartialViewResult SelectAccessory( int id)
        {
            var input = new SAMS.WorkOrders.Dtos.GetDetailInput() { Id = id };
            ViewBag.WorkOrder=_workOrderAppService.GetDetail(input);
            return PartialView("_SelectAccessory");
        }
        //配件选择列表
        public PartialViewResult GetAccessory(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var where=JsonConvert.DeserializeObject<dynamic>(param.Where);
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var where=serializer.Deserialize< dynamic>(param.Where);
            
            var viewModel = new GetAccessoryViewModel();
            GetNewStaffStockInput input = new GetNewStaffStockInput() {
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting,
                StaffID = where["UserId"]
            };
            viewModel.Accessories = _inventoryAppService.GetStaffStock(input);
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;


            return PartialView("_GetAccessory", viewModel);
        }
        [HttpPost]
        public ContentResult Dispatch(FormCollection form)
        {
            int billID;
            long UserId;
            int.TryParse(form["id"], out billID);
            long.TryParse(form["nextHandlerID"], out UserId);
            var input = new DispatchInput() { BillId= billID, UserId= UserId };
            _workOrderAppService.Dispatch(input);
            //_workOrderAppService.(id);
            return Content("ok");
        }
        [HttpPost]
        public ContentResult ReturnVisit(FormCollection form)
        {
            int billID;
            int Evalution;
            int.TryParse(form["ID"], out billID);
            int.TryParse(form["Evalution"],out Evalution);
            string isResolved=form["IsResolved"];
            string desc = form["Description"];
            var input = new ReturnVisitInput() { BillId = billID, Evaluation = (EvaluationEnum)Evalution,Resolved =false,ReturnVisitDesc=desc};
            _workOrderAppService.ReturnVisit(input);
            return Content("ok");
        }

        public ActionResult PopAlbum() {

            if (TempData["tempId"]!=null)
            {

                var input = new SAMS.WorkOrders.Dtos.GetDetailInput() { Id = (int)TempData["tempId"] };
                var workOrder = _workOrderAppService.GetDetail(input);
                var model = new PictureViewModel() { WorkOrder = workOrder };
                return View(model);

            }
            return View();

        }
    }
}