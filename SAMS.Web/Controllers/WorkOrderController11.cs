using SAMS.Faults;

using SAMS.Web.Models.WorkOrder;
using SAMS.WorkOrders;
using SAMS.WorkOrders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Controllers
{
    public class WorkOrderController11 : Controller
    {
        private readonly IWorkOrderAppService _workOrderAppService;
        private readonly IFaultAppService _faultAppService;

        public WorkOrderController11(IWorkOrderAppService workOrderAppService, IFaultAppService faultAppService)
        {

            _workOrderAppService = workOrderAppService;
            _faultAppService = faultAppService;
          
        }

        // GET: WorkOrder
        public ActionResult Index()
        {
            return View();
        }

        //详细
        public ActionResult Detail(int id)
        {
            var input = new GetDetailInput() { Id = id };
            var workOrder =  _workOrderAppService.GetDetail(input);
            return View(new WorkOrderViewModel() { bill = workOrder });
        }

    
        //汇报
        public ActionResult Edit(int id)
        {
            var input = new GetDetailInput() { Id = id };
            var workOrder =  _workOrderAppService.GetDetail(input);

            var faults =  _faultAppService.GetAllDetails();
            return View(new WorkOrderEditModel() { bill = workOrder,faults= faults });
        }
        
    }
}