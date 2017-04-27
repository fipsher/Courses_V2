using System.Web.Mvc;

namespace Courses_v2.Areas.Admin
{
    public class LecturerAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Lecturer";

        public override void RegisterArea(AreaRegistrationContext context) => context.MapRoute(
                "Lecturer_default",
                "Lecturer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
    }
}