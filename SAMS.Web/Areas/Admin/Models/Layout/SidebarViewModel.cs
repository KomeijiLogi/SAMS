using Abp.Application.Navigation;

namespace SAMS.Web.Areas.Admin.Models.Layout
{
    public class SidebarViewModel
    {
        public UserMenu Menu { get; set; }

        public string CurrentPageName { get; set; }
    }
}