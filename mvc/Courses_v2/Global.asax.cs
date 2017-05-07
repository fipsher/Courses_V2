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
            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "NotFound";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
            if (Context.Response.StatusCode == 500)
            {
                Response.Clear();

                var rd = new RouteData();
                rd.DataTokens["area"] = ""; // In case controller is in another area
                rd.Values["controller"] = "Error";
                rd.Values["action"] = "InternalServer";

                IController c = new ErrorController();
                c.Execute(new RequestContext(new HttpContextWrapper(Context), rd));
            }
        }
    }
}
