using System.Web;
using System.Web.Mvc;

namespace WebCarRace
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AjaxPartialFilter());
        }

        public class AjaxPartialFilter : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {

                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    var oldView = filterContext.Result as ViewResult;
                    if (oldView != null)
                    {
                        filterContext.Result = new PartialViewResult
                        {
                            ViewData = oldView.ViewData,
                            ViewName = oldView.ViewName
                        };
                    }
                }
            }

        }
    }
}
