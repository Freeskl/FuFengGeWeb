using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace FuFengGe.Jurisdiction
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]//allmultiple容许多个标签同时起作用  
    public class JurisdictionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 日志文件
        /// </summary>
        private readonly Logger _logger;

        private string _type;


        /// <summary>
        /// 有且仅仅用来进行登录判断
        /// </summary>
        /// <param name="type">The type.</param>
        public JurisdictionAttribute(string type)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _type = type;
        }

        //action执行之前先执行此方法  
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userid=filterContext.RequestContext.HttpContext.Session["userid"];
            var userName = filterContext.RequestContext.HttpContext.Session["username"];
            if (userid==null|| userName==null)
            {
                ForceLogin(filterContext);
            }
        }

        //action执行之后先执行此方法  
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ;
        }

        //actionresult执行之前执行此方法  
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ;
        }

        //actionresult执行之后执行此方法  
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ;

        }

        #region 私有方法

        /// <summary>
        /// 强制登录
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <author>
        /// 2018/1/25 张侠飞
        /// </author>
        private void ForceLogin(ActionExecutingContext filterContext)
        {
            switch (_type)
            {
                case "View":
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new{Controller ="User",Action ="Login",referurl =filterContext.RequestContext.HttpContext.Request.Url}));
                    break;
                case "Json":
                    filterContext.Result =new JsonResult()
                    {
                        Data=new
                        {
                            type=-1,
                            redicthref = new UrlHelper(filterContext.RequestContext).Action("Login", "User", new { referurl = filterContext.RequestContext.HttpContext.Request.UrlReferrer })
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    break;
            }
        }

        #endregion
    }
}