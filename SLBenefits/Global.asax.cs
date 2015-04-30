using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SLBenefits.App_Start;

namespace SLBenefits
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Start();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            if (Request.ApplicationPath != "/"
                    && Request.ApplicationPath.Equals(Request.Path, StringComparison.CurrentCultureIgnoreCase))
            {
                var redirectUrl = VirtualPathUtility.AppendTrailingSlash(Request.ApplicationPath);
                Response.RedirectPermanent(redirectUrl);
            }
        }
    }
}
