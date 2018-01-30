using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace FuFengGe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private   Logger _logger ;

        protected void Application_Start()
        {
            //网站api访问，可添加webapi
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _logger = LogManager.GetCurrentClassLogger();
            _logger.Info("网站启动");
        }


        protected void Application_Stop()
        {
            _logger.Info("网站终止");
        }
    }
}
