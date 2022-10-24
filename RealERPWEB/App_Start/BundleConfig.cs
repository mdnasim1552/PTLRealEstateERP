using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace RealERPWEB
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951

      
        public static void RegisterBundles(BundleCollection bundles)
        {

           // BundleTable.EnableOptimizations = false;            
            bundles.Add(new ScriptBundle("~/bundles/Version").Include(
                "~/Scripts/jquery-2.1.3.min.js",  
                 "~/Scripts/jquery-ui.min.js",
                  "~/Scripts/bootstrap.min.js",
                  "~/Scripts/moment-with-locales.js",
                    "~/Scripts/bootstrap-datetimepicker.js",
                    "~/Scripts/Extension.min.js"
              ));
            
            bundles.Add(new ScriptBundle("~/bundles/Versionnew").Include(
                "~/Content/Theme/vendor/jquery/jquery.min.js",
                "~/Content/Theme/vendor/jquery/jquery-ui.min.js",
                "~/Content/Theme/theme.min.js",  
                 "~/Content/Theme/vendor/bootstrap/js/popper.min.js",
                 "~/Content/Theme/vendor/bootstrap/js/bootstrap.min.js",
                 "~/Content/Theme/vendor/pace/pace.min.js",
                 "~/Content/Theme/vendor/stacked-menu/stacked-menu.min.js",
                 "~/Content/Theme/vendor/perfect-scrollbar/perfect-scrollbar.min.js",
                 "~/Content/Theme/vendor/flatpickr/flatpickr.min.js",
                 "~/Content/Theme/vendor/easy-pie-chart/jquery.easypiechart.min.js",
                 "~/Content/Theme/vendor/chart.js/Chart.min.js",
                 //"~/Content/Theme/theme.min.js",
                 "~/Content/Theme/vendor/toastr/toastr.min.js",
                 //"~/Content/Theme/dashboard-demo.js",
                 "~/Content/Theme/vendor/toastr/toastr-demo.js"                            
              ));
           
           

            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include(
               // "~/Scripts/jqplot.dateAxisRenderer.min.js",
               // "~/Scripts/jqplot.highlighter.min.js",
               //"~/Scripts/jquery.jqplot.min.js",
               // "~/Scripts/jqplot.cursor.min.js",  
                 "~/Scripts/jquery.tablesorter.min.js",
                 "~/Scripts/jquery-ui.js",
                 "~/Scripts/jquery-migrate-1.3.0.js",
               "~/Scripts/jquery.tablesorter.widgets.js"));


            
         // Common
            bundles.Add(new ScriptBundle("~/bundles/gridviewscroll").Include(               
               "~/Scripts/ScrollableGridPlugin.js",
               "~/Scripts/gridviewScrollHaVertworow.min.js",
                "~/Scripts/gridviewScrollHaVer.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/tablescroll").Include(              
              "~/Scripts/ScrollableTablePlugin.js"));


            bundles.Add(new ScriptBundle("~/bundles/chosen").Include(
             "~/Scripts/chosen.jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/keynavigation").Include(
           "~/Scripts/jquery.keynavigation.js"));

            bundles.Add(new ScriptBundle("~/bundles/richtext").Include(
          "~/tinymce/tinymce.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include(
             "~/Scripts/bootstrap-multiselect.js",
             "~/Scripts/select2.min.js"
             ));

              bundles.Add(new ScriptBundle("~/bundles/User").Include(
                "~/JS02/RealERPScript.js",
                "~/Scripts/KeyPress.js"
                
                ));

            bundles.Add(new ScriptBundle("~/bundles/Counter").Include(
                "~/Scripts/waypoints.min.js",
                "~/Scripts/jquery.counterup.min.js"                
                ));

            bundles.Add(new ScriptBundle("~/bundles/highchart").Include(
               "~/Scripts/highchartwithmap.js",
               "~/Scripts/highchartexporting.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/flatpickr").Include(
               "~/assets/vendor/flatpickr/flatpickr.min.js",
               "~/Scripts/flatpickr-demo.js"
               ));

            //bundles.Add(new ScriptBundle("~/bundles/telephoneValidation").Include(
            //  "~/assets/TelephoneValidation/build/js/intlTelInput.js"
            // // "~/assets/TelephoneValidation/js/isValidNumber.js"
            //  ));


            //bundles.Add(new  StyleBundle("~/Content/css").Include(
            //    "~/Content/bootstrap.icon-large.css",
            //    "~/Content/flaticon.css",
            //    "~/Content/asitCommonStyle.css",
            //    "~/Content/AppsStyle.css",
            //    "~/Content/AsitnoneResponsive.css",
            //    "~/Content/screen.css",
            //    "~/Content/ResponsiveForm.css",
            //    "~/Content/chosen.css",
            //    "~/Content/wheelmenu.css",
            //    "~/Content/GridViewScrooling.css",
            //    "~/Content/progressbar.css",
            //    "~/Content/bootstrap-multiselect.css",
            //    "~/Content/theme.blue.css",
            //    "~/Content/animate.css"

            //    ));
            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));
        }
    }
}