using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses_v2.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult InternalServer() => View();

        public ActionResult NotFound() => View();

        public ActionResult AccessDenied() => View();
    }
}