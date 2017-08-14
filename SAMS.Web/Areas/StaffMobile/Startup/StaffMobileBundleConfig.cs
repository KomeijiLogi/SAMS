using System.Web.Optimization;
using SAMS.Web.Bundling;

namespace SAMS.Web.Areas.StaffMobile.Startup
{
    public static class StaffMobileBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                         // "~/Scripts/jquery-1.10.2.min.js",
                          "~/Scripts/qingjs.js",
                          "~/Scripts/mui.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/mui.min.css"));
            bundles.Add(new StyleBundle("~/Content/search").Include(
                    "~/Content/Search.css"));
            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                       "~/Scripts/home.js"));
            bundles.Add(new ScriptBundle("~/bundles/detail").Include(
                      "~/Scripts/detail.js"));
            bundles.Add(new ScriptBundle("~/bundles/index").Include(
                      "~/Scripts/index.js"));
            bundles.Add(new ScriptBundle("~/bundles/pluginchosen").Include(
                      "~/Scripts/pluginChosen.js"));
            bundles.Add(new ScriptBundle("~/bundles/receipt").Include(
                      "~/Scripts/receipt.js"));
            bundles.Add(new StyleBundle("~/Content/receiptcss").Include(
                      "~/Content/receipt.css"));
            bundles.Add(new StyleBundle("~/Content/detailcss").Include(
                      "~/Content/detail.css"));

            bundles.Add(new StyleBundle("~/Content/homecss").Include(
                        "~/Content/home.css"));
            bundles.Add(new ScriptBundle("~/bundles/history").Include(
                        "~/Scripts/history.js"));

            bundles.Add(new ScriptBundle("~/bundles/historydetail").Include(
                         "~/Scripts/HistoryDetail.js"));
            bundles.Add(new StyleBundle("~/Content/mine").Include(
                        "~/Scripts/mine.css"));

            bundles.Add(new ScriptBundle("~/bundles/muiindexlist").Include(
                         "~/Scripts/mui.indexedlist.js"));
            bundles.Add(new StyleBundle("~/Content/muiindexlistcss").Include(
                        "~/Content/mui.indexedlist.css"));
        }
    }
}