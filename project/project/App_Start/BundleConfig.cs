using System.Web;
using System.Web.Optimization;

namespace project
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            /***** Scripts *****/

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            /* SB-Admin Flot */
            bundles.Add(new ScriptBundle("~/Content/jquery-flot").Include(
                       "~/Content/sb-admin-2/bower_components/flot/jquery.flot.js"));
            /* SB-Admin Flot Pie */
            bundles.Add(new ScriptBundle("~/Content/jquery-flot-pie").Include(
                       "~/Content/sb-admin-2/bower_components/flot/jquery.flot.pie.js"));

            bundles.Add(new ScriptBundle("~/Content/jquery-datatables").Include(
                       "~/Content/sb-admin-2/bower_components/datatables/media/js/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/Content/jquery-morris").Include(
                       "~/Content/sb-admin-2/bower_components/morrisjs/morris.js"));

            bundles.Add(new ScriptBundle("~/Content/jquery-raphael").Include(
                      "~/Content/sb-admin-2/bower_components/raphael/raphael.js"));

            /* Twitter Bootstrap JS */
            bundles.Add(new ScriptBundle("~/Content/bootstrap").Include(
                       "~/Content/sb-admin-2/bower_components/bootstrap/dist/js/bootstrap.js"));

            /* Dashboard Flot Pie JS */
            bundles.Add(new ScriptBundle("~/Scripts/clients-flot-pie").Include(
                       "~/Scripts/dashboard/clients-flot-pie.js"));

            bundles.Add(new ScriptBundle("~/Scripts/products-flot-pie").Include(
                       "~/Scripts/dashboard/products-flot-pie.js"));

            bundles.Add(new ScriptBundle("~/Scripts/top-sales-datatable").Include("~/Scripts/dashboard/top-sales-datatable.js"));

            bundles.Add(new ScriptBundle("~/Scripts/global-financial-morris").Include("~/Scripts/dashboard/global-financial-morris.js"));

            bundles.Add(new ScriptBundle("~/Scripts/shipments").Include("~/Scripts/dashboard/shipments.js"));

            /***** CSS *****/

            /* Twitter Bootstrap CSS */
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                       "~/Content/sb-admin-2/bower_components/bootstrap/dist/css/bootstrap.css"));

            /* Main Dash CSS */
            bundles.Add(new StyleBundle("~/Content/css/Home").Include("~/Content/css/Home.css"));
            bundles.Add(new StyleBundle("~/Content/css/jquery-datatables").Include("~/Content/sb-admin-2/bower_components/datatables/media/css/jquery.dataTables.css"));

            bundles.Add(new StyleBundle("~/Content/css/site").Include("~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}