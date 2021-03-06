﻿using System.Web;
using System.Web.Optimization;

namespace MainSite
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-wizard.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                        "~/Scripts/sidebar-nav.js",
                        "~/Scripts/custom.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/raphael-min.js",
                        "~/Scripts/morris.js",
                        "~/Scripts/site.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/sidebar-nav.css",
                      "~/Content/wizard.css",
                      "~/Content/animate.css",
                      "~/Content/sweetalert.css",
                      "~/Content/morris.css",
                      "~/Content/calculator.css",
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/site").Include(
            //"~/Scripts/site.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

        }
    }
}
