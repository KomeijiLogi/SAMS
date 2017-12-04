using Abp.Web.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SAMS.EquipmentArchives;
using SAMS.EquipmentArchives.Dto;
using SAMS.Products;
using SAMS.Products.Dtos;
using SAMS.Users;
using SAMS.Web.Areas.Admin.Models.Common;
using SAMS.Web.Areas.Admin.Models.EquipmentArchive;
using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
    public class EquipmentArchiveController: SAMSControllerBase
    {
        private readonly IEquipmentArchiveAppService _equipmentArchiveAppService;
        private readonly IProductAppService _productAppService;
        private readonly UserManager _userManager;

        public EquipmentArchiveController(IEquipmentArchiveAppService equipmentArchiveAppService
            , IProductAppService productAppService
            , UserManager userManager)
        {
            _equipmentArchiveAppService = equipmentArchiveAppService;
            _productAppService = productAppService;
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            var products = _productAppService.GetProducts();
         
            var viewModel = new IndexViewModel()
            {
                Products = products.Items.ToArray<ProductListDto>()
            };
            return View(viewModel);
        }
        public PartialViewResult Get(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
           
            GetArchiveWhere where = null;
            if (!string.IsNullOrEmpty(param.Where))
                where = JsonConvert.DeserializeObject<GetArchiveWhere>(param.Where);

            GetEquipmentArchivesInput input = new GetEquipmentArchivesInput() {
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting,
                CustomerName=where.CustomerName,
                EndInstallDate=where.EndInstallDate,
                EndServiceDate=where.EndServiceDate,
                EndWarrenty=where.EndWarrenty,
                ProductId=where.ProductId,
                SerialNo=where.SerialNo,
                StartInstallDate=where.StartInstallDate,
                StartServiceDate=where.StartServiceDate,
                StartWarrenty=where.StartWarrenty
            };
            var viewModel = new GetModel()
            {
                EquipmentArchives = _equipmentArchiveAppService.GetEquipmentArchives(input),
                PageSize = param.PageSize,
                PageIndex=param.PageIndex
            };
            



            return PartialView("_Get", viewModel);
        }
        public PartialViewResult Create()
        {
            var viewModel = new ArchiveModel();
            var products = _productAppService.GetProducts();
            viewModel.Products = products.Items;
            var users = _userManager.Users.ToList();
            var userList = new SelectList(users, "ID", "Name");
            viewModel.PersonItemList = userList;
            return PartialView("_Create", viewModel);
        }
        [HttpPost]
        public ContentResult Create(ArchiveModel model)
        {
            var input = new CreateEquipmentArchiveInput()
            {
                AssignedPersonId = model.AssignedPersonId,
                CustomerId=model.CustomerId,
                InstallType=model.InstallType,
                Office=model.Office,
                OfficeMobile=model.OfficeMobile,
                OfficePerson=model.OfficePerson,
                OfficePosition=model.OfficePosition,
                OfficeTel=model.OfficeTel,
                ProductId=model.ProductId,
                SerialNo=model.SerialNo,
                ServiceTime=model.ServiceTime,
                Warrenty=model.Warrenty
            };
            _equipmentArchiveAppService.Create(input);
            return Content("ok");
        }
        [HttpPost]
        public ContentResult Delete(int id)
        {
            _equipmentArchiveAppService.Delete(id);
            return Content("ok");
        }
        [DontWrapResult]
        public ActionResult CheckSerialNo(int? id,string serialNo)
        {
           var result=_equipmentArchiveAppService.CheckSerialNo(id,serialNo);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int id)
        {
            var archive = _equipmentArchiveAppService.GetDetail(id);
            var products = _productAppService.GetProducts();
            //var selectItemList = new SelectList();
            var productArr = products.Items.ToArray<ProductListDto>();
            var Items = productArr.Select(x => new ProductListDto()
            {
                Name = x.Name + "_" + x.Model,
                Id = x.Id

            }).ToList();
            //selectItemList.AddRange(new SelectList(Items, "Id", "Name", archive.ProductId ));
           // var viewModel = new DetailModel();
            
            //viewModel.ProductItemList= new SelectList(Items, "Id", "Name", archive.ProductId);
            var users = _userManager.Users.ToList();
            var userList = new SelectList(users, "ID", "Name", archive.AssignedPersonId );
            //viewModel.PersonItemList = userList;

            var viewModel = new DetailModel()
            {
                AssignedPersonId = archive.AssignedPersonId,
                AssignedPersonName = archive.AssignedPersonName,
                CustomerCode = archive.CustomerCode,
                CustomerId = archive.CustomerId,
                CustomerName = archive.CustomerName,
                Id = archive.Id,
                InstallType = archive.InstallType,
                LastServiceTime = archive.LastServiceTime,
                Office = archive.Office,
                OfficeMobile = archive.OfficeMobile,
                OfficePerson = archive.OfficePerson,
                OfficePosition = archive.OfficePosition,
                OfficeTel = archive.OfficeTel,
                PersonItemList = userList,
                ProductCode = archive.ProductCode,
                ProductId = archive.ProductId,
                ProductItemList = new SelectList(Items, "Id", "Name", archive.ProductId),
                ProductModel = archive.ProductModel,
                ProductName = archive.ProductName,
                SerialNo = archive.SerialNo,
                ServiceTime = archive.ServiceTime,
                Warrenty = archive.Warrenty


            };
            return View(viewModel);
        }

       
        [HttpPost]
        public ContentResult Edit(ArchiveEditParm model)
        {
            EditEquipmentArchiveInput input = new EditEquipmentArchiveInput()
            {
                Id = model.Id,
                AssignedPersonId=model.AssignedPersonId,
                CustomerId=model.CustomerId,
                ProductId=model.ProductId,
                InstallType=model.InstallType,
                Office=model.Office,
                OfficeMobile=model.OfficeMobile,
                OfficePerson=model.OfficePerson,
                OfficePosition=model.OfficePosition,
                OfficeTel=model.OfficeTel,
                SerialNo=model.SerialNo,
                ServiceTime=model.ServiceTime,
                Warrenty=model.Warrenty
            };
            _equipmentArchiveAppService.Edit(input);
            return Content("ok");

        }
        public PartialViewResult Edit(int id)
        {
            var archive = _equipmentArchiveAppService.GetDetail(id);
            var products = _productAppService.GetProducts();
            //var selectItemList = new SelectList();
            var productArr = products.Items.ToArray<ProductListDto>();
            var Items = productArr.Select(x => new ProductListDto()
            {
                Name = x.Name + "_" + x.Model,
                Id = x.Id

            }).ToList();

            var users = _userManager.Users.ToList();
            var userList = new SelectList(users, "ID", "Name", archive.AssignedPersonId);
            var viewModel = new DetailModel()
            {
                AssignedPersonId = archive.AssignedPersonId,
                AssignedPersonName=archive.AssignedPersonName,
                CustomerCode=archive.CustomerCode,
                CustomerId=archive.CustomerId,
                CustomerName=archive.CustomerName,
                Id=archive.Id,
                InstallType=archive.InstallType,
                LastServiceTime=archive.LastServiceTime,
                Office=archive.Office,
                OfficeMobile=archive.OfficeMobile,
                OfficePerson=archive.OfficePerson,
                OfficePosition=archive.OfficePosition,
                OfficeTel=archive.OfficeTel,
                PersonItemList= userList,
                ProductCode=archive.ProductCode,
                ProductId=archive.ProductId,
                ProductItemList= new SelectList(Items, "Id", "Name", archive.ProductId),
                ProductModel=archive.ProductModel,
                ProductName=archive.ProductName,
                SerialNo=archive.SerialNo,
                ServiceTime=archive.ServiceTime,
                Warrenty=archive.Warrenty


            };
            return PartialView("_Edit",viewModel);
        }
    }
   
    internal class GetArchiveWhere
    {
        public string SerialNo { get; set; }
        public string CustomerName { get; set; }
        public string ProductId { get; set; }
        public DateTime? StartInstallDate { get; set; }
        public DateTime? EndInstallDate { get; set; }
        public DateTime? StartServiceDate { get; set; }
        public DateTime? EndServiceDate { get; set; }
        public DateTime? StartWarrenty { get; set; }
        public DateTime? EndWarrenty { get; set; }
    }
}