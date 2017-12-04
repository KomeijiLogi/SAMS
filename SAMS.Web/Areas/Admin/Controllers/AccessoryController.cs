using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SAMS.Accessories;
using SAMS.Accessories.Dtos;
using SAMS.Inventory;
using SAMS.Inventory.Dto;
using SAMS.Users;
using SAMS.Web.Areas.Admin.Models.Accessory;
using SAMS.Web.Areas.Admin.Models.Common;
using SAMS.Web.Areas.Admin.Models.WorkOrder;
using SAMS.Web.Controllers;
using SAMS.WorkOrders;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SAMS.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize(Roles ="Admin")]
    public class AccessoryController : SAMSControllerBase
    {
        private readonly IInventoryAppService _inventoryAppService;
        private readonly IAccessoryAppService _accessoryAppService;
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly UserManager _userManager;
        public AccessoryController(IInventoryAppService inventoryAppService, IAccessoryAppService accessoryAppService,
            IWorkOrderAppService workOrderAppService ,UserManager userManager)
        {
            _inventoryAppService = inventoryAppService;
            _accessoryAppService = accessoryAppService;
            _workOrderAppService = workOrderAppService;
            _userManager = userManager;
        }
        // GET: Admin/Accessory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewOrgStock()
        {
            return View();
        }

        /// <summary>
        /// 备件入库
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult CreateNewEnterStockOrder(string type)
        {
            ViewBag.Type = type;
            if (type.Equals("EnterStockNew"))
            {
                ViewBag.CategoryID = 2;//系统入库
                ViewBag.Title = "备件入库";
                ViewBag.UserList = null;


            }
            else if (type.Equals("StaffRefundNew"))
            {
                ViewBag.Title = "员工退料";
                ViewBag.CategoryID = 4;//员工退料
                var users = _userManager.Users.ToList();
                var userList = new SelectList(users, "ID", "Name");
                ViewBag.UserList = userList;
            }

            ViewBag.StockType = 2;//入库
            return View();
        }
        /// <summary>
        /// 备件出库
        /// </summary>
        /// <param name="type">ExportStockNew 、StaffPickingNew</param>
        /// <returns></returns>
        public ActionResult CreateNewExportStockOrder(string type)
        {
            ViewBag.Type = type;
            if (type.Equals("ExportStockNew"))
            {
                ViewBag.Title = "备件出库";
                ViewBag.CategoryID = 1;//系统出库
                ViewBag.UserList = null;
            }
            else if (type.Equals("StaffPickingNew"))
            {
                ViewBag.Title = "员工领料";
                var users = _userManager.Users.ToList();
                var userList = new SelectList(users, "ID", "Name");
                ViewBag.UserList = userList;
                ViewBag.CategoryID = 3;//员工领料
            }
            ViewBag.StockType = 1;//出库
            return View();
        }
        public PartialViewResult SelectNewAccessory(string type, long? staffId)
        {
            ViewBag.Type = type;
            if (staffId.HasValue)
                ViewBag.StaffId = staffId.Value;
            else
                ViewBag.StaffId = "";
            return PartialView("_SelectNewAccessory");
        }
        public PartialViewResult GetNewAccessory(GetAccessoryListParam param)
        {
            string type = param.Type;
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewAccessoryViewModel();

            //配件入库          
            if (type.Equals("EnterStockNew"))
            {   
                
                GetAccessoryInput input = new GetAccessoryInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting};
                if (((string)(JObject.Parse(param.Where)["searchkey"])) != "")
                {
                    JObject jsob = JObject.Parse(param.Where);
                    string jval = (string)jsob["searchkey"];

                    input = new GetAccessoryInput() { MaxResultCount = 1000, SkipCount = skipCount, Sorting = sorting };
                    var accessoryList = _accessoryAppService.GetAccessory(input);
                    viewModel.Accessories = new PagedResultDto<GetAccessoryCount>
                    {
                        Items = accessoryList.Items.Where(x => x.AccessoryNumber.Contains(jval)).Select(x => new GetAccessoryCount
                        {
                            AccessoryId = x.AccessoryId,
                            AccessoryName = x.AccessoryName,
                            AccessoryModel = x.AccessoryModel,
                            AccessoryNumber = x.AccessoryNumber,
                            AccessoryUnit = x.AccessoryUnit,
                            Count = 0
                        }
                        )
                        .ToList(),
                        TotalCount = accessoryList.TotalCount,

                    };

                }
                else {

                    var accessoryList = _accessoryAppService.GetAccessory(input);
                    viewModel.Accessories = new PagedResultDto<GetAccessoryCount>
                    {
                        Items = accessoryList.Items.Select(x => new GetAccessoryCount
                        {
                            AccessoryId = x.AccessoryId,
                            AccessoryName = x.AccessoryName,
                            AccessoryModel = x.AccessoryModel,
                            AccessoryNumber = x.AccessoryNumber,
                            AccessoryUnit = x.AccessoryUnit,
                            Count = 0
                        }
                        )
                        .ToList(),
                        TotalCount = accessoryList.TotalCount,

                    };



                }

            }
            else if (type.Equals("StaffRefundNew"))//员工退料
            {
                long staffID = param.StaffID.Value;
                GetNewStaffStockInput input = new GetNewStaffStockInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting, StaffID = staffID };

                var accessoryList = _inventoryAppService.GetStaffStock(input);
                viewModel.Accessories = new PagedResultDto<GetAccessoryCount>
                {
                    Items = accessoryList.Items.Select(x => new GetAccessoryCount
                    {
                        AccessoryId = x.AccessoryId,
                        AccessoryName = x.AccessoryName,
                        AccessoryModel = x.AccessoryModel,
                        AccessoryNumber = x.AccessoryNumber,
                        AccessoryUnit = x.AccessoryUnit,
                        Count = x.Count
                    }
                    )
                    .ToList(),
                    TotalCount = accessoryList.TotalCount,

                };
            }
            else if (type.Equals("StaffPickingNew"))//员工领料
            {
                // long staffID = param.StaffID.Value;

                GetOrgStockInput input = new GetOrgStockInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting };
                var accessoryList = _inventoryAppService.GetOrgStock(input);
                viewModel.Accessories = new PagedResultDto<GetAccessoryCount>
                {
                    Items = accessoryList.Items.Select(x => new GetAccessoryCount
                    {
                        AccessoryId = x.AccessoryId,
                        AccessoryName = x.AccessoryName,
                        AccessoryModel = x.AccessoryModel,
                        AccessoryNumber = x.AccessoryNumber,
                        AccessoryUnit = x.AccessoryUnit,
                        Count = x.Count
                    }
                   )
                   .ToList(),
                    TotalCount = accessoryList.TotalCount,

                };
            }
            else if (type.Equals("ExportStockNew"))//配件出库
            {
                GetOrgStockInput input = new GetOrgStockInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting };
                var accessoryList = _inventoryAppService.GetOrgStock(input);
                viewModel.Accessories = new PagedResultDto<GetAccessoryCount>
                {
                    Items = accessoryList.Items.Select(x => new GetAccessoryCount
                    {
                        AccessoryId = x.AccessoryId,
                        AccessoryName = x.AccessoryName,
                        AccessoryModel = x.AccessoryModel,
                        AccessoryNumber = x.AccessoryNumber,
                        AccessoryUnit = x.AccessoryUnit,
                        Count = x.Count
                    }
                   )
                   .ToList(),
                    TotalCount = accessoryList.TotalCount,

                };
            }
            viewModel.Type = type;
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewAccessory", viewModel);
        }
        public PartialViewResult GetNewOrgStock(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewOrgStockViewModel();
            GetOrgStockInput input = new GetOrgStockInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting };
            viewModel.Accessories = _inventoryAppService.GetOrgStock(input);
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewOrgStock", viewModel);
        }

        /// <summary>
        /// 提交配件入库
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult CreateNewEnterStockOrder(FormCollection form)
        {
            var input = new CreateNewEnterStockOrderInput();
            //入库
            input.StockType = 2;
            long userId;
            if (form["StockOperateType"].Equals("StaffRefundNew"))
            {
                input.CategoryID = 4;
                if (long.TryParse(form["RecipientID"], out userId))
                {
                    input.StraffID = userId;
                }
            }
            else
            {
                input.CategoryID = 2;
            }

            input.Description = form["Remark"];
            //if (int.TryParse(form["StockType"], out stockType))
            //{
            //    input.StockType = stockType;
            //}

            input.StockBillEntry = new List<StockBillEntryDto>();
            var entries = JsonConvert.DeserializeObject<dynamic>(form["detail"]) as JArray;
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = new StockBillEntryDto();
                var item = (JObject)JsonConvert.DeserializeObject(entries[i].ToString());
                entry.AccessoryId = (string)item["AccessoryID"];
                entry.Count = (int)item["Count"];
                input.StockBillEntry.Add(entry);
            }


            _inventoryAppService.CreateNewEnterStockOrder(input);

            return Content("ok");
        }

        /// <summary>
        /// 提交配件出库
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ContentResult CreateNewExportStockOrder(FormCollection form)
        {
            var input = new CreateNewExportStockOrderInput();
            //入库
            input.StockType = 1;
            long userId;
            if (form["StockOperateType"].Equals("StaffPickingNew"))
            {
                input.CategoryID = 3;
                if (long.TryParse(form["RecipientID"], out userId))
                {
                    input.StraffID = userId;
                }
            }
            else if (form["StockOperateType"].Equals("ExportStockNew"))
            {
                input.CategoryID = 1;
            }
            input.Description = form["Remark"];

            input.StockBillEntry = new List<StockBillEntryDto>();
            var entries = JsonConvert.DeserializeObject<dynamic>(form["detail"]) as JArray;
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = new StockBillEntryDto();
                var item = (JObject)JsonConvert.DeserializeObject(entries[i].ToString());
                entry.AccessoryId = (string)item["AccessoryID"];
                entry.Count = (int)item["Count"];
                input.StockBillEntry.Add(entry);
            }


            _inventoryAppService.CreateNewExportStockOrder(input);
            return Content("ok");
        }

        public ActionResult NewExportEnterStockOrder()
        {
            return View();
        }
        public PartialViewResult GetNewExportEnterStockOrder(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewExportEnterStockOrderViewModel();
            GetNewExportEnterStockOrderInput input = new GetNewExportEnterStockOrderInput() {
                MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting };
            viewModel.Orders = _inventoryAppService.GetNewExportEnterStockOrder(input);
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewExportEnterStockOrder", viewModel);
        }
        public PartialViewResult NewExportEnterStockOrderDetail(int id)
        {
            var viewModel = new NewExportEnterStockOrderDetailViewModel();
            viewModel.bill = _inventoryAppService.GetStockBillDetail(id);
            return PartialView("_NewExportEnterStockOrderDetail", viewModel);

        }
        public ActionResult NewStaffPicking()
        {
            var users = _userManager.Users.ToList();
            var userList = new SelectList(users, "ID", "Name");
            ViewBag.UserList = userList;
            return View();
        }
        /// <summary>
        ///  领料记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PartialViewResult GetNewStaffPicking(GetAccessoryListParam param)
        {
            string type = param.Type;
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewStaffPickingViewModel();
            GetNewStaffPickingInput input = new GetNewStaffPickingInput() {
                StaffID =param.StaffID.Value,
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting
            } ;
            viewModel.Orders = _inventoryAppService.GetNewStaffPicking(input);
       
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewStaffPicking", viewModel);

        }
        /// <summary>
        /// 退料记录
        /// </summary>
         public PartialViewResult GetNewStaffRefund(GetAccessoryListParam param)
        {
            string type = param.Type;
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewStaffRefundViewModel();
            GetNewStaffRefundInput input = new GetNewStaffRefundInput()
            {
                StaffID = param.StaffID.Value,
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting
            };
            viewModel.Orders = _inventoryAppService.GetNewStaffRefund(input);

            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewStaffRefund", viewModel);

        }
        /// <summary>
        /// 使用记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PartialViewResult GetNewStaffUse(GetAccessoryListParam param)
        {
            string type = param.Type;
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewStaffUseViewModel();
            GetNewStaffUseInput input = new GetNewStaffUseInput()
            {
                StaffID = param.StaffID.Value,
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting
            };
            viewModel.Orders = _workOrderAppService.GetNewStaffUse(input);

            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewStaffUse", viewModel);
        }

        /// <summary>
        /// 持有备件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PartialViewResult GetNewStaffStock(GetAccessoryListParam param)
        {
            string type = param.Type;
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewStaffStockViewModel();
            GetNewStaffStockInput input = new GetNewStaffStockInput()
            {
                StaffID = param.StaffID.Value,
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting
            };
            viewModel.Accessories = _inventoryAppService.GetStaffStock(input);

            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewStaffStock", viewModel);
        }



        public ActionResult NewAccessoryUse()
        {
           
            return View();
        }
        /// <summary>
        /// 备件使用明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public PartialViewResult GetNewAccessoryUse(GetAccessoryListParam param)
        {
            string type = param.Type;
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetNewAccessoryUseViewModel();
            var where = JsonConvert.DeserializeObject<GetNewAccessoryUseWhere>(param.Where);
            GetNewAccessoryUseInput input = new GetNewAccessoryUseInput()
            {
                Where = where,
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting
            };
            viewModel.Accessories = _workOrderAppService.GetNewAccessoryUse(input);

            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            return PartialView("_GetNewAccessoryUse", viewModel);
        }
    }
}