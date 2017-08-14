using Abp.Web.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Areas.Admin.Controllers
{
      [AbpMvcAuthorize(Roles ="Admin")]
    public class SystemSettingController : Controller
    {
        // GET: Admin/SystemSetting
        public ActionResult Index()
        {
            return View();
        }
    }
}