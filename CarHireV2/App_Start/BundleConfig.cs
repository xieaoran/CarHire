using System.Web;
using System.Web.Optimization;

namespace CarHireV2
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Content/js/jquery/*.js"));
            bundles.Add(new ScriptBundle("~/js/metro/common").Include( "~/Content/js/metro/common/*.js"));
            bundles.Add(new ScriptBundle("~/js/carHire/common").Include("~/Content/js/carHire/common/*.js"));
            bundles.Add(new StyleBundle("~/css/common").Include("~/Content/css/common/*.css"));
        }
    }
}
