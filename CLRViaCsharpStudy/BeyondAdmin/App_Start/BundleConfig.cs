using System.Web;
using System.Web.Optimization;

namespace BeyondAdmin
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

                BundleTable.EnableOptimizations = false;
                BundleTable.Bundles.IgnoreList.Clear();
                //公用控件js
                bundles.Add(new ScriptBundle("~/Scripts/publicControl/Base/js").Include(
                   "~/Scripts/Common.js",
                   "~/Scripts/PublicControlsJS/MemberObjectControl.js",
                   "~/Scripts/PublicControlsJS/SubjectControl.js",
                   "~/Scripts/PublicControlsJS/FeeControl.js",
                   "~/Scripts/PublicControlsJS/FeeTypeControl.js",
                   "~/Scripts/PublicControlsJS/ShopControl.js"
                   ));


                //Home基础CSS
                bundles.Add(new StyleBundle("~/Hadmin/base/Home/css").Include(
                            "~/Content/HAdmin/css/bootstrap.min.css",
                            "~/Content/HAdmin/css/font-awesome.css",
                            "~/Content/HAdmin/css/animate.css",
                            "~/Content/HAdmin/css/style.css"
                            ));
                //Home基础JS
                bundles.Add(new ScriptBundle("~/Hadmin/base/Home/js").Include(
                    "~/Scripts/HAdmin/jquery.min.js",
                    "~/Scripts/HAdmin/bootstrap.min.js",
                    "~/Scripts/HAdmin/plugins/metisMenu/jquery.metisMenu.js",
                    "~/Scripts/HAdmin/plugins/slimscroll/jquery.slimscroll.min.js",
                    "~/Scripts/HAdmin/plugins/layer/layer.min.js",
                    "~/Scripts/HAdmin/hAdmin.js",
                    "~/Scripts/HAdmin/contabs.min.js",
                    "~/Scripts/HAdmin/Index.js",
                    "~/Scripts/HAdmin/respond.min.js"
                    ));

                //Home第三方插件
                bundles.Add(new ScriptBundle("~/Hadmin/base/Home/pace/js").Include(
                    "~/Scripts/HAdmin/plugins/pace/pace.min.js"
                    ));


                //layout基础CSS
                bundles.Add(new StyleBundle("~/Hadmin/base/layout/css").Include(
                            "~/Content/HAdmin/css/plugins/jsTree/style.min.css",
                            "~/Content/HAdmin/bootstrap-select2/css/select2.css",
                            "~/Content/HAdmin/css/plugins/bootstrap-table/bootstrap-table.min.css",
                            "~/Content/Index/Index.css"
                            ));
                //layout基础JS
                bundles.Add(new ScriptBundle("~/Hadmin/base/layout/js").Include(
                    "~/Scripts/HAdmin/content.js",
                    "~/Scripts/HAdmin/plugins/jsTree/jstree.min.js",
                    "~/Scripts/HAdmin/bootstrap-select2/js/select2.min.js",
                    "~/Scripts/HAdmin/plugins/bootstrap-table/bootstrap-table.min.js",
                    "~/Scripts/HAdmin/plugins/bootstrap-table/locale/bootstrap-table-zh-CN.min.js"
                    ));



                //登录css
                bundles.Add(new StyleBundle("~/Hadmin/Login/css").Include(
                    "~/Content/HAdmin/css/login.css"
                    ));
            }
        }
    }

