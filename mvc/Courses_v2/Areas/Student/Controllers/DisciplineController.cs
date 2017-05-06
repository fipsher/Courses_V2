using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;
using static Core.Enums.Enums;

namespace Courses_v2.Areas.Student.Controllers
{
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        private readonly IUserService _userService;
        private readonly ICathedraService _cathedraService;

        public DisciplineController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.DisciplineService)
        {
            _userService = serviceFactory.UserService;
            _cathedraService = serviceFactory.CathedraService;
        }

        public ActionResult Index(SearchFilter<Discipline> filter = null)
        {
            filter = filter == null || filter.OptionList == null ? SearchFilter<Discipline>.Default : filter;
            var disciplines = Service.FindDisciplineResponse(filter);
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
            var user = _userService.Find(new SearchFilter<User>()
            {
                OptionList = new[] { new User { Id = _userId } }
            }).SingleOrDefault();
            var disciplines = Service.FindDisciplineResponse(new SearchFilter<Discipline>
            {
                OptionList = user.RegisteredDisciplines.Select(rd => new Discipline { Id = rd.DisciplineId })
            });
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
                return View();
            }
        }
    }
}
