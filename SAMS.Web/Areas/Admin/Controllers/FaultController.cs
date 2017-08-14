using Abp.Web.Mvc.Authorization;
using SAMS.Faults;
using SAMS.Faults.Dtos;
using SAMS.Web.Areas.Admin.Models.Fault;
using SAMS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class FaultController: SAMSControllerBase
    {
        private readonly IFaultAppService _faultAppService;
        public FaultController(IFaultAppService faultAppService)
        {
            _faultAppService = faultAppService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ContentResult Delete(int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                _faultAppService.Delete(ids[i]);
            }
            return Content("ok");
        }
        public PartialViewResult Get()
        {
            var viewModel = new GetViewModel();

            viewModel.Faults = _faultAppService.GetFaults();
            return PartialView("_Get", viewModel);
        }
        public PartialViewResult Create()
        {

            var fault = new Models.Fault.Fault();

            return PartialView("_Create", fault);
        }
        [HttpPost]
        public ContentResult Create(Models.Fault.Fault fault)
        {
            var createInput = new CreateInput()
            {
              
                Name = fault.Name,
              
            };
            _faultAppService.Create(createInput);
            return Content("ok");
        }
        [HttpPost]
        public ContentResult Edit(Models.Fault.Fault fault)
        {
            var editInput = new EditInput()
            {
                Id = fault.Id.Value,
                Name = fault.Name
            };
            _faultAppService.Edit(editInput);
            return Content("ok");
        }
        [HttpGet]
        public PartialViewResult Edit(int id)
        {
         
            var fault = _faultAppService.GetDetail(id);
            var viewModel = new Models.Fault.Fault()
            {
                Id = fault.Id,
                Name = fault.Name
            };

            return PartialView("_Edit", viewModel);
        }


    }
}