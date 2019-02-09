using System.Web;
using System.Web.Optimization;

namespace JAG.DevTest2019.Host
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                    .Include("~/Content/bootstrap.css")
                    .Include("~/Content/custom.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/modernizr-{version}.js",
                "~/Scripts/custom.js"));
        }
    }
}
