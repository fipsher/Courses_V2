using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Core.Enums.Enums;

namespace Courses_v2.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        public DisciplineController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.DisciplineService)
        {
        }

        public ActionResult Index(int pageIndex = 0)
        {
            var filter = SearchFilter<Discipline>.Default;
            filter.Skip = pageIndex * filter.Take;
            var disciplines = Service.FindDisciplineResponse(filter);
            var user = ServiceFactory.UserService.Find(SearchFilter<User>.FilterById(UserId)).SingleOrDefault();
            
            ViewBag.User = user;
            return View(disciplines);
        }

        public ActionResult Details(string id)
        {
            var disciplines = Service.FindDisciplineResponse((new SearchFilter<Discipline>
            {
                OptionList = new[] { new Discipline() { Id = id } }
            }));

            return View(disciplines.SingleOrDefault());
        }

        public ActionResult MyDisciplines()
        {
            var user = ServiceFactory.UserService.Find(SearchFilter<User>.FilterById(UserId)).SingleOrDefault();
            var disciplines = Service.FindDisciplineResponse(
                                      SearchFilter<Discipline>.FilterByIds(user.DisciplineIds
                                                                               .Select(rd => rd.DisciplineId)));
            return View(disciplines);
        }

        public ActionResult Register(string id)
        {
            var result = Service.RegisterStudent(_userId, id);
            ViewBag.RegisterResult = result ? "Успішно зареєстровано" : "Упс, щось не так";
            return View();
        }

        public ActionResult Unregister(string id)
        {
            try
            {
                Service.UnregisterStudent(_userId, id);
                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }
    }
}
