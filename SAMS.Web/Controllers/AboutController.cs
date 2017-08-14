using System.Web.Mvc;

namespace SAMS.Web.Controllers
{
    public class AboutController : SAMSControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}