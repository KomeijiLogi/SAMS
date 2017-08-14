using Abp.Web.Mvc.Authorization;
using Newtonsoft.Json;
using SAMS.Accessories;
using SAMS.Accessories.Dtos;
using SAMS.Web.Areas.Admin.Models.AccessorySetting;
using SAMS.Web.Areas.Admin.Models.Common;
using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class AccessorySettingController : SAMSControllerBase
    {
        private readonly IAccessoryAppService _accessoryAppService;
        public AccessorySettingController( IAccessoryAppService accessoryAppService
           )
        {
            _accessoryAppService = accessoryAppService;
        }
        // GET: Admin/AccessorySetting
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Get(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetViewModel();
            //var where = JsonConvert.DeserializeObject<GetCustomersWhere>(param.Where);

            GetAccessoryInput input = new GetAccessoryInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting };

            viewModel.Accessories = _accessoryAppService.GetAccessory(input);
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;

            return PartialView("_Get", viewModel);
        }
        //删除
        public ContentResult Delete(int id)
        {
            
           _accessoryAppService.Delete(id);
        
            return Content("ok");
        }
        //新建
        public ActionResult Create()
        {

            var viewModel = new CreateViewModel();

            //viewModel.Products = _productAppService.GetProducts();


            return View("Create", viewModel);
        }
        //编辑
        public ActionResult Edit(int id)
        {

            var accessory = _accessoryAppService.GetDetail(id);
            var viewModel = new EditViewModel()
            {
                Id = accessory.Id,
                Number = accessory.Number,
                Unit = accessory.Unit,
                Model = accessory.Model,
                Name = accessory.Name
            };
            return View("Edit", viewModel);
        }
        [HttpPost]
        public ContentResult Create(CreateViewModel input)
        {
            var createInput = new CreateInput()
            {
                Number = input.Number,
                Unit = input.Unit,
                Name = input.Name,
                Model = input.Model
            };
            _accessoryAppService.Create(createInput);
            return Content("ok");
        }
        [HttpPost]
        public ContentResult Edit(EditViewModel accessory)
        {
            var editInput = new EditInput()
            {
                Id = accessory.Id,
                Model = accessory.Model,
                Name = accessory.Name,
                Unit = accessory.Unit,
                Number = accessory.Number
            };
            _accessoryAppService.Edit(editInput);
            return Content("ok");
        }
    }
}