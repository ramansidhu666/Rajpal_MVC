using System.Web;
using System.Web.Optimization;

namespace Rajpal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Content/js/jquery-1.12.4.min.js",
                         "~/Content/js/plugins.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Content/js/custom.js"));

            bundles.Add(new StyleBundle("~/Content/home").Include(
                     "~/Content/css/plugins.css",
                       "~/Content/css/style.css"));
            bundles.Add(new StyleBundle("~/Content/custom").Include(
                    "~/Content/css/Custom.css",
                     "~/Content/css/bootstrap.min.css",
                       "~/Content/css/paging.css"));
        }
    }
}
