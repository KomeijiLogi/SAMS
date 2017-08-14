using SAMS.Sessions.Dto;

namespace SAMS.Web.Areas.Admin.Models.Layout
{
    public class FooterViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string GetProductNameWithEdition()
        {
            var productName = "WOne";

            

            return productName;
        }
    }
}