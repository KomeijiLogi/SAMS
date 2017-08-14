using System.Web.Optimization;
using SAMS.Web.Bundling;

namespace SAMS.Web.Areas.Admin.Startup
{
    public static class AdminBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //LIBRARIES

            AddAdminCssLibs(bundles, false);
            AddAdminCssLibs(bundles, true);

            bundles.Add(
                new ScriptBundle("~/Bundles/Admin/libs/js")
                    .Include(
                        ScriptPaths.Json2,
                        ScriptPaths.JQuery,
                        ScriptPaths.JQuery_Migrate,
                        ScriptPaths.JQuery_UI,
                        ScriptPaths.JQuery_Validation,
                        ScriptPaths.Bootstrap,
                        ScriptPaths.Bootstrap_Hover_Dropdown,
                        ScriptPaths.JQuery_Slimscroll,
                        ScriptPaths.JQuery_BlockUi,
                        ScriptPaths.JQuery_Cookie,
                        ScriptPaths.JQuery_Uniform,
                        ScriptPaths.JQuery_Ajax_Form,
                        ScriptPaths.JQuery_jTable,
                        ScriptPaths.JQuery_Color,
                        ScriptPaths.JQuery_Jcrop,
                        //ScriptPaths.SignalR,
                        ScriptPaths.Morris,
                        ScriptPaths.Morris_Raphael,
                        ScriptPaths.JQuery_Sparkline,
                        ScriptPaths.JsTree,
                        ScriptPaths.Bootstrap_Switch,
                        ScriptPaths.SpinJs,
                        ScriptPaths.SpinJs_JQuery,
                        ScriptPaths.SweetAlert,
                        ScriptPaths.Toastr,
                        ScriptPaths.MomentJs,
                        ScriptPaths.Bootstrap_DateRangePicker,
                        ScriptPaths.Bootstrap_Select,
                        ScriptPaths.Underscore,
                        ScriptPaths.Abp,
                        ScriptPaths.Abp_JQuery,
                        ScriptPaths.Abp_Toastr,
                        ScriptPaths.Abp_BlockUi,
                        ScriptPaths.Abp_SpinJs,
                        ScriptPaths.Abp_SweetAlert,
                        ScriptPaths.Abp_jTable,
                        ScriptPaths.MustacheJs,
                        ScriptPaths.Select2,
                        ScriptPaths.Select2_zh_CN,
                        ScriptPaths.JQuery_Unobtrusive,
                        ScriptPaths.JQuery_Unobtrusive_validate,
                        ScriptPaths.Bootstrap_Growl,
                        ScriptPaths.Bootstrap_Box
                    ).ForceOrdered()
                );

            //COMMON (for MPA)
            bundles.Add(
                new ScriptBundle("~/Bundles/Admin/Common/js")
                   // .IncludeDirectory("~/Areas/Admin/Common/Scripts", "*.js", true)
                   // .Include("~/Areas/Admin/Views/Common/Modals/_LookupModal.js")
                    .ForceOrdered()
                );

            //METRONIC
            AddAppMetrinicCss(bundles, isRTL: false);
            AddAppMetrinicCss(bundles, isRTL: true);

            bundles.Add(
              new ScriptBundle("~/Bundles/Admin/metronic/js")
                  .Include(
                      //"~/metronic/assets/global/scripts/app.js",
                      //"~/metronic/assets/admin/layout/scripts/layout.js",
                      "~/metronic/assets/global/scripts/metronic.js",
                      "~/content/admin/scripts/layout.js"
                  ).ForceOrdered()
              );
        }

        private static void AddAdminCssLibs(BundleCollection bundles, bool isRTL)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/Admin/libs/css" + (isRTL ? "RTL" : ""))
                    .Include(StylePaths.JQuery_UI, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.JQuery_jTable_Theme, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.FontAwesome, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.Simple_Line_Icons, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.FamFamFamFlags, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(isRTL ? StylePaths.BootstrapRTL : StylePaths.Bootstrap, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.JQuery_Uniform, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.JsTree, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.Morris)
                    .Include(StylePaths.SweetAlert)
                    .Include(StylePaths.Toastr)
                    .Include(StylePaths.Bootstrap_DateRangePicker)
                    .Include(StylePaths.Bootstrap_Switch)
                    .Include(StylePaths.Bootstrap_Select)
                    .Include(StylePaths.JQuery_Jcrop)
                    .Include(StylePaths.Select2)
                    
                    .ForceOrdered()
                );
        }

        private static void AddAppMetrinicCss(BundleCollection bundles, bool isRTL)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/Admin/metronic/css" + (isRTL ? "RTL" : ""))
                    .Include("~/metronic/assets/global/css/components-rounded" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/global/css/plugins-md" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/admin/layout/css/layout" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    //.Include("~/metronic/assets/admin/layout/css/themes/light" + (isRTL ? "-rtl" : "") + ".css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/admin/layout/css/custom.css" , new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/admin/layout/css/themes/default.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/admin/layout/css/dataTables.bootstrap.css")
                    .ForceOrdered()
                );
        }
    }
}