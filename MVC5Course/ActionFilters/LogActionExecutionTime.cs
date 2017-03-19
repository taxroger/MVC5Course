using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.ActionFilters
{
    public class LogActionExecutionTimeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.startTime = DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.endTime = DateTime.Now;
            //filterContext.Result =
            filterContext.Controller.ViewBag.duration =
                filterContext.Controller.ViewBag.endTime -
                filterContext.Controller.ViewBag.startTime;
        }
    }
}