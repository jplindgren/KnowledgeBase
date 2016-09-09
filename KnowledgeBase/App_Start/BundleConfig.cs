using System.Web;
using System.Web.Optimization;

namespace KnowledgeBase {
    public class BundleConfig {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles) {

            bundles.UseCdn = true; 
            
            var jqueryCdnPath = "//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js";
            var cacheKey = "d9b3dfd57f8c40c9-b3b294615be571d4";
            var myKnowledgeCdn = "http://myknowledge.azureedge.net/{0}?" + cacheKey;

            bundles.Add(new ScriptBundle("~/bundles/jquery", jqueryCdnPath).Include("~/Scripts/vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/javascript", string.Format(myKnowledgeCdn, "bundles/javascript")).Include(
                        "~/Scripts/vendor/bootstrap.js",
                        "~/Scripts/vendor/radio.min.js",
                        "~/Scripts/vendor/jquery.bootstrap-autohidingnavbar.js",
                        "~/Scripts/vendor/shortcut.js",
                        "~/Scripts/vendor/toastr.js",
                        "~/Scripts/app/serializeObject.js"));

            bundles.Add(new ScriptBundle("~/bundles/javascript/app", string.Format(myKnowledgeCdn, "bundles/javascript/app")).Include(
                        "~/Scripts/main.js",
                        "~/Scripts/app/application.js",
                        "~/Scripts/app/myShortcuts.js",
                        "~/Scripts/app/article-list.js",
                        "~/Scripts/app/search-input.js",
                        "~/Scripts/app/tag-menu.js",
                        "~/Scripts/app/create-article-popup.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/vendor/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/css", string.Format(myKnowledgeCdn, "bundles/css")).Include("~/Content/css/*.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/bundles/css/main", string.Format(myKnowledgeCdn, "bundles/css/main")).Include("~/Content/css/app/main.css"));
            bundles.Add(new StyleBundle("~/bundles/css/blank_main").Include("~/Content/css/app/blank_main.css"));


            BundleTable.EnableOptimizations = true;
        }
    } //class
}