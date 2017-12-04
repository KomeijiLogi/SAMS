using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using Newtonsoft.Json;
using SAMS.Customers;
using SAMS.Customers.Dtos;
using SAMS.Web.Areas.Admin.Models.Common;
using SAMS.Web.Areas.Admin.Models.Customer;

using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class CustomerController : SAMSControllerBase
    {
        private readonly ICustomerAppService _customerAppService;
        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            var input=new GetDetailInput() { Id = id };
            var customer = _customerAppService.GetDetail(input);
            var viewModel = new DetailsViewModel();
            viewModel.Customer = customer;
            viewModel.LastServiceTime = _customerAppService.GetLastServiceTime(id);
            
            return View(viewModel);
        }
        public PartialViewResult Get(GetListParam param)
        {
            int skipCount = param.PageSize * (param.PageIndex - 1);
            string sorting = null;
            if (!string.IsNullOrEmpty(param.Sort))
                sorting = param.Sort + " " + param.Direction;
            var viewModel = new GetViewModel();
            GetCustomersWhere where = null;
            if(!string.IsNullOrEmpty(  param.Where))
                where= JsonConvert.DeserializeObject<GetCustomersWhere>(param.Where);

            GetCustomersInput input = new GetCustomersInput() { MaxResultCount = param.PageSize, SkipCount = skipCount, Sorting = sorting, Where = where };
            
            viewModel.Customers = _customerAppService.GetCustomers(input);
            viewModel.PageIndex = param.PageIndex;
            viewModel.PageSize = param.PageSize;
            
            return PartialView("_Get", viewModel);
        }
        //public PartialViewResult Edit(string id)
        //{
        //    var input = new GetDetailInput() { Id = id };
        //    var customer = _customerAppService.GetDetail(input);
        //    var viewModel = new GetDetailViewModel()
        //    {
        //        Address = customer.Address,
        //        Area=customer.Area,
        //        Name=customer.Name,
        //        Mobile=customer.Mobile,
        //        Id=customer.Id,
        //        Email=customer.Email,
        //        CreationTime=customer.CreationTime,
        //        Description=customer.Description,
        //        Number1=customer.Number1,
        //        Number2=customer.Number2
        //    };

        //    return PartialView("_Edit", viewModel);
        //}
        //public PartialViewResult Create()
        //{

        //    return PartialView("_Create");
        //}
        //[HttpPost]
        //public ContentResult Create(CreateViewModel input)
        //{
        //    var createInput = new CreateInput()
        //    {
        //        Address = input.Address,
        //        Area=input.Area,
        //        Description=input.Description,
        //        Email=input.Email,
        //        Mobile=input.Mobile,
        //        Name=input.Name,
        //        Number1=input.Number1,
        //        Number2=input.Number2
        //    };
        //    _customerAppService.Create(createInput);
        //    return Content("ok");
        //}
        //[HttpPost]
        //public ContentResult Edit(EditViewModel model)
        //{

        //    var editInput = new EditInput()
        //    {
        //        Id = model.Id,
        //        Address = model.Address,
        //        Area = model.Area,
        //        Description = model.Description,
        //        Email = model.Email,
        //        Mobile = model.Mobile,
        //        Name = model.Name,
        //        Number1 = model.Number1,
        //        Number2 = model.Number2
        //    };
        //    _customerAppService.Edit(editInput);
        //    return Content("ok");
        //}
        //[HttpPost]
        //public ContentResult Delete(int ids)
        //{
        //    _customerAppService.Delete(ids);
        //    return Content("ok");
        //}
        //[HttpPost]
        //public ContentResult Delete(int[] ids)
        //{
        //    for (int i = 0; i < ids.Length; i++)
        //    {
        //        _customerAppService.Delete(ids[i]);
        //    }
        //    return Content("ok");
        //}

     
        [DontWrapResult]
        public JsonResult GetCustomersByName(string name)
        {
            var customers = _customerAppService.GetCustomerByName(name);
            if (customers.Items.Count == 0)
            {
                List<GetCustomerByNameDto> list = new List<GetCustomerByNameDto>();
                list.Add(new GetCustomerByNameDto() { Name = name });
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(customers.Items, JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult GetCustomersUL(string name)
        {
            var customers = _customerAppService.GetCustomerByName(name);
            ViewBag.Customers = customers;
            return PartialView("_CustomerUL");
        }

    }
}