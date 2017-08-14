using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using SAMS.Products;
using SAMS.Products.Dtos;
using SAMS.Web.Areas.Admin.Models.Product;
using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAMS.Web.Areas.Admin.Models.AccessorySetting;
using SAMS.Accessories.Dtos;
using SAMS.Accessories;
using SAMS.Web.Areas.Admin.Models.Common;
namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class ProductController : SAMSControllerBase
    {
        private readonly IProductAppService _productAppService;
        private readonly IAccessoryAppService _accessoryAppService;
        public ProductController(IProductAppService productAppService, IAccessoryAppService accessoryAppService
           )
        {
            _productAppService = productAppService;
            _accessoryAppService = accessoryAppService;
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            //var viewModel = new GetViewModel();

            //viewModel.Products = _productAppService.GetProducts();
            return View();
        }
       

        public PartialViewResult Get()
        {
            var viewModel = new SAMS.Web.Areas.Admin.Models.Product.GetViewModel();

            viewModel.Products = _productAppService.GetProducts();
            return PartialView("_Get", viewModel);
        }
        public PartialViewResult Create()
        {

            var viewModel = new SAMS.Web.Areas.Admin.Models.Product.CreateViewModel();
            
            //viewModel.Products = _productAppService.GetProducts();


            return PartialView("_Create", viewModel);
        }
        [HttpPost]
        public ContentResult Create(SAMS.Web.Areas.Admin.Models.Product.CreateViewModel input)
        {
            var createInput = new SAMS.Products.Dtos.CreateInput()
            {
                EASNumber=input.EASNumber,
                K3Number=input.K3Number,
                Name=input.Name,
                Model=input.Model
            };
            _productAppService.Create(createInput);
            return Content("ok");
        }
        [HttpPost]
        public ContentResult Edit(SAMS.Web.Areas.Admin.Models.Product.EditViewModel product)
        {
            var editInput = new SAMS.Products.Dtos.EditInput()
            {
                Id = product.Id.Value,
                Model = product.Model,
                Name = product.Name,
                K3Number = product.K3Number,
                EASNumber = product.EASNumber
            };
            _productAppService.Edit(editInput);
            return Content("ok");
        }
        [HttpPost]
        public ContentResult SaveBom(SaveBomInput input)
        {
            var accessorys=input.Accessorys.Select(e => e.AccessoryId);
            _productAppService.UpdateBoms(input.ProductId, accessorys);
            return Content("ok");
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            var input = new GetDetailInput() { Id = id };
            var product = _productAppService.GetDetail(input);
            var viewModel = new SAMS.Web.Areas.Admin.Models.Product.EditViewModel()
            {
               Id=product.Id,
               EASNumber=product.EASNumber,
               K3Number=product.K3Number,
               Model=product.Model,
               Name=product.Name
            };

            return PartialView("_Edit", viewModel);
        }

        public PartialViewResult Bom(int id)
        {
            var boms = _productAppService.GetBom(id);
            var product = _productAppService.GetDetail(new GetDetailInput() { Id = id });
            var viewModel = new BomViewModel() { Boms = boms , Product = product };
            return PartialView("_Bom", viewModel);
        }
        //选择配件
        public PartialViewResult SelectAccessory()
        {
            return PartialView("_SelectAccessory");
        }
        //配件选择列表
        public PartialViewResult GetAccessory(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetAccessoryViewModel();
            SAMS.Accessories.Dtos.GetAccessoryInput input = new SAMS.Accessories.Dtos.GetAccessoryInput() {
                MaxResultCount = param.PageSize,
                SkipCount = skipCount,
                Sorting = sorting
            };

            viewModel.Accessories = _accessoryAppService.GetAccessory(input);
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;

            return PartialView("_GetAccessory", viewModel);
        }
        //[HttpPost]
        //public ContentResult Edit1(int id,string name,string model)
        //{
        //    var editInput = new EditInput()
        //    {
        //        Id = id,
        //        Model = name,
        //        Name = model

        //    };
        //    _productAppService.Edit(editInput);
        //    return Content("ok");
        //}

        [HttpPost]
        public ContentResult Delete(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                _productAppService.Delete(ids[i]);
            }
            return Content("ok");
        }
        [DontWrapResult]
        public JsonResult CheckProduct(string name,string model,int? id)
        {

            bool isSuccess = true;
            var products=_productAppService.GetProductsByNameModel(name, model);
            if(products.Items.Count>0)
            {
                if (!id.HasValue)
                    isSuccess = false;
                else
                    isSuccess = products.Items[0].Id == id;
                
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);

        }



    }
}