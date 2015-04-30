using System.Web.Optimization;

namespace SLBenefits.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/angular-route.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                .Include("~/Scripts/angular-resource.js")
                .Include("~/Scripts/angular-animate.js")
                .Include("~/Scripts/angular-sanitize.js")
                 );

            bundles.Add(new ScriptBundle("~/bundles/app")
                    .Include("~/App/App.js")
                    //Controllers
                    .Include("~/App/Controllers/dashboardController.js")
                    .Include("~/App/Controllers/categoryController.js")
                    //Directives
                    .Include("~/App/Directives/customDirectives.js")
                    
              );

            bundles.Add(new ScriptBundle("~/bundles/externallib")
                .Include("~/Scripts/datetimepicker/datetimepicker.js")
                .Include("~/Scripts/jquery.cookie.js",
                         "~/Scripts/noty/packaged/jquery.noty.packaged.min.js",
                         "~/Scripts/noty/layouts/topCenter.js",
                         "~/Scripts/noty/themes/default.js")
                );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/datetimepicker/datetimepicker.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/previewcss").Include(
                      "~/Content/bootstrap.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/watgresources").Include(
                     "~/resources.watg/ace-fonts.css",
                     "~/resources.watg/ace.min.css",
                     "~/resources.watg/ace-rtl.min.css",
                     "~/resources.watg/ace-skins.min.css",
                     "~/resources.watg/style-watg.css"
                     ));
        }
    }
}
