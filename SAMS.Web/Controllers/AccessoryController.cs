using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAMS.Web.Controllers
{
    public class AccessoryController : Controller
    {
        // GET: Accessory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewOrgStock()
        {
            return View();
        }
    }
}