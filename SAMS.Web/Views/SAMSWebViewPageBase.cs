using Abp.Web.Mvc.Views;

namespace SAMS.Web.Views
{
    public abstract class SAMSWebViewPageBase : SAMSWebViewPageBase<dynamic>
    {

    }

    public abstract class SAMSWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected SAMSWebViewPageBase()
        {
            LocalizationSourceName = SAMSConsts.LocalizationSourceName;
        }
    }
}