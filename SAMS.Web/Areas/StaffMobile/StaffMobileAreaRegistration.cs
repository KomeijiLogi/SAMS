using System.Web.Mvc;

namespace SAMS.Web.Areas.StaffMobile
{
    public class StaffMobileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StaffMobile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StaffMobile_default",
                "StaffMobile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
                
            );
        }
    }
}