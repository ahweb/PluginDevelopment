using System.Web.Optimization;

namespace PluginDevelopment
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //添加AnchorJS文档操作js插件
            bundles.Add(new ScriptBundle("~/bundles/autocjs").Include(
                "~/Scripts/autoc.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //添加富文本编辑器
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/ueditor.config.js",
                      "~/Scripts/ueditor.all.js",
                      "~/Scripts/ueditor.parse.js",
                      "~/Scripts/zh-cn.js"));

            bundles.Add(new ScriptBundle("~/bundles/textedit").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/autoc.css",
                      "~/Content/style.css"));
        }
    }
}
