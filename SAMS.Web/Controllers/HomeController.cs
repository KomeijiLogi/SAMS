using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace SAMS.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : SAMSControllerBase
    {
        public ActionResult Index()
        {
            return Redirect("admin/workorder");
            //return View();
        }
	}
}