using Courses_v2.Controllers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Courses_v2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_EndRequest()
        {
#if !DEBUG
            switch (Context.Response.StatusCode)
            {
                case 404:
                    Redirect(string.Empty, "Error", "NotFound");
                    break;

                case 500:
                    Redirect(string.Empty, "Error", "InternalServer");
                    break;

                case 401:
                    Redirect(string.Empty, "Error", "AccessDenied");
                    break;
            }
#endif
        }

        private void Redirect(string area, string controller, string action)
        {
            Response.Clear();

            var rd = new RouteData();
            rd.DataTokens["area"] = area; // In case controller is in another area
            rd.Values["controller"] = controller;
            rd.Values["action"] = action;

            IController c = new ErrorController();
            c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
        }
    }
}
