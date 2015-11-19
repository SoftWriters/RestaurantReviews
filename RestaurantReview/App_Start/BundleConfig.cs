namespace RestaurantReview.Web
{
    using RestaurantReview.Web.Constants;
    using System.Web.Optimization;

    public static class BundleConfig
    {
        /// <summary>
        /// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Enable Optimizations
            // Set EnableOptimizations to false for debugging. For more information,
            // Web.config file system.web/compilation[debug=true]
            // OR
            // BundleTable.EnableOptimizations = true;

            // Enable CDN usage.
            // Note: that you can choose to remove the CDN if you are developing an intranet application.
            // Note: We are using Google's CDN where possible and then Microsoft if not available for better performance (Google is more likely to have been cached by the users browser).
            // Note: that protocol (http:) is ommited from the CDN URL on purpose to allow the browser to choose the protocol.
            bundles.UseCdn = false;

            AddCss(bundles);
            AddJavaScript(bundles);
        }

        private static void AddCss(BundleCollection bundles)
        {
            // Bootstrap - Twitter Bootstrap CSS (http://getbootstrap.com/).
            // Font Awesome - Icons using font (http://fortawesome.github.io/Font-Awesome/).
            // Site - Your custom site css.
            // Note: No CDN support has been added here. Most likely you will want to customize your copy of bootstrap.
            // Note: If you host any of your CSS on a seperate domain (Like a CDN), then be sure to fix an issue with respond.js which stops working for IE8.
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/fuel-ux.css",
                "~/Content/bootstrap-site.css",
                "~/Content/fontawesome-site.css",
                "~/Content/site.css",
                "~/Content/datatables.css"));
        }

        /// <summary>
        /// Creates and adds JavaScript bundles to the bundle collection.
        /// Content Delivery Network's (CDN) are used where available.
        ///
        /// Note: MVC's built in
        /// <see cref="System.Web.Optimization.Bundle.CdnFallbackExpression"/> is not used as using inline
        /// scripts is not permitted under Content Security Policy (CSP) (See FilterConfig for more details).
        ///
        /// Instead, we create our own failover bundles. If a CDN is not reachable, the failover script
        /// loads the local bundles instead. The failover script is only a few lines of code and should have
        /// a minimal impact, although it does add an extra request (Two if the browser is IE8 or less).
        /// If you feel confident in the CDN availability and prefer better performance, you can delete these lines.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        private static void AddJavaScript(BundleCollection bundles)
        {
            // jQuery - The JavaScript helper API (http://jquery.com/).
            Bundle jqueryBundle = new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-2.1.3.js");
            bundles.Add(jqueryBundle);

            Bundle jqueryBundleOld = new ScriptBundle("~/bundles/jqueryold")
                .Include("~/Scripts/jquery-1.11.3.js");
            bundles.Add(jqueryBundleOld);

            // jQuery Validate - Client side JavaScript form validation (http://jqueryvalidation.org/).
            Bundle jqueryValidateBundle = new ScriptBundle("~/bundles/jqueryval")
                .Include("~/Scripts/jquery.validate.js");
            bundles.Add(jqueryValidateBundle);

            Bundle jqueryValidateCustomValidatorsBundler = new ScriptBundle("~/bundles/jqueryvalcustomvalidators")
                .Include("~/Scripts/jquery.validate.requirenotif.js")
                .Include("~/Scripts/jquery.validate.mustnotequal.js");
            bundles.Add(jqueryValidateCustomValidatorsBundler);

            // Microsoft jQuery Validate Unobtrusive - Validation using HTML data- attributes (http://stackoverflow.com/questions/11534910/what-is-jquery-unobtrusive-validation)
            Bundle jqueryValidateUnobtrusiveBundle = new ScriptBundle("~/bundles/jqueryvalunobtrusive")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/validate_extensions.js");
            bundles.Add(jqueryValidateUnobtrusiveBundle);

            Bundle jqueryAjaxUnobtrusive = new ScriptBundle("~/bundles/jqueryajaxunobtrusive")
                .Include("~/Scripts/jquery.unobtrusive.ajax.js");
            bundles.Add(jqueryAjaxUnobtrusive);

            // Modernizr - Allows you to check if a particular API is available in the browser (http://modernizr.com).
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            // Note: The current version of Modernizr does not support Content Security Policy (CSP) (See FilterConfig).
            // In addition the CDN version is old. So we are using a local copy which skips some CSP violating checks
            // and returns true for them. This is REALLY bad and needs to be fixed soon. See here for details:
            // https://github.com/Modernizr/Modernizr/pull/1263 and http://stackoverflow.com/questions/26532234/modernizr-causes-content-security-policy-csp-violation-errors
            Bundle modernizrBundle = new ScriptBundle("~/bundles/modernizr") // , ContentDeliveryNetwork.Microsoft.ModernizrUrl
                //.Include("~/Scripts/modernizr-*");
                .Include("~/Scripts/modernizr-*");
            bundles.Add(modernizrBundle);

            // Bootstrap - Twitter Bootstrap JavaScript (http://getbootstrap.com/).
            Bundle bootstrapBundle = new ScriptBundle("~/bundles/bootstrap")
                .Include("~/Scripts/bootstrap.js");
            bundles.Add(bootstrapBundle);

            // FuelUX - JavaScript (http://getfuelux.com) //www.fuelcdn.com/fuelux/3.5.0/js/fuelux.min.js
            Bundle fueluxBundle = new ScriptBundle("~/bundles/fuelux")
                .Include("~/Scripts/fuelux/*.js");
            bundles.Add(fueluxBundle);

            // Respond.js - A fast & lightweight polyfill for min/max-width CSS3 Media Queries (https://github.com/scottjehl/Respond).
            // Note: that the CDN version is a little behind the latest 1.4.2.
            Bundle respondBundle = new ScriptBundle("~/bundles/respond")
                .Include("~/Scripts/respond.js");
            bundles.Add(respondBundle);

            Bundle pluginsStart = new ScriptBundle("~/bundles/plugins_start")
               //.Include("~/Scripts/plugins/inline-edit.js")
               .Include("~/Scripts/plugins/handlebars-v3.0.0.js")
               .Include("~/Scripts/plugins/typeahead.bundle.js")
               .Include("~/Scripts/moment.js")
               .Include("~/Scripts/plugins/jquery.dataTables.js")
               .Include("~/Scripts/plugins/jquery.dataTables.hideEmptyColumns.js")
               .Include("~/Scripts/plugins/jquery.dataTables.rowGrouping.js");
            bundles.Add(pluginsStart);

            Bundle pluginsEnd = new ScriptBundle("~/bundles/plugins_end")
                .Include("~/Scripts/plugins/bootstrap-datepicker.js");
                //.Include("~/Scripts/plugins/bootstrap-datetimepicker.js")
                //.Include("~/Scripts/plugins/bootstrap-fileupload.js");
            bundles.Add(pluginsEnd);

            // Failover Core - If the CDN's fail then these scripts load local copies of the same scripts.
            Bundle failoverCoreBundle = new ScriptBundle("~/bundles/failovercore")
                .Include("~/Scripts/Failover/jquery.js")
                .Include("~/Scripts/Failover/jqueryval.js")
                .Include("~/Scripts/Failover/jqueryvalunobtrusive.js")
                .Include("~/Scripts/Failover/bootstrap.js");
            bundles.Add(failoverCoreBundle);

            // Failover Respond - If the Respond.js CDN fails then this script loads a local copy.
            // Note: This script is only used if the browser is running IE8 or less.
            Bundle failoverRespondBundle = new ScriptBundle("~/bundles/failoverrespond")
                .Include("~/Scripts/Failover/respond.js");
            bundles.Add(failoverRespondBundle);
        }
    }
}