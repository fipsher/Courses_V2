using Core;
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

        public ActionResult SocioDisciplines(int pageIndex = 0)
        {
            var user = ServiceFactory.UserService.Find(SearchFilter<User>.FilterById(UserId)).SingleOrDefault();

            var filter = SearchFilter<Discipline>.Default;
            filter.Skip = pageIndex * filter.Take;
            filter.OptionList = FilterHelper.SocialDisciplines(user.Course);

            var disciplines = Service.FindDisciplineResponse(filter);
            
            SetDataToViewBag(user);
            return View(disciplines);
        }

        public ActionResult SpecialDisciplines(int pageIndex = 0)
        {
            var user = ServiceFactory.UserService.Find(SearchFilter<User>.FilterById(UserId)).SingleOrDefault();

            var filter = SearchFilter<Discipline>.Default;
            filter.Skip = pageIndex * filter.Take;
            filter.OptionList = FilterHelper.SpecialDisciplines(user.Course);

            var disciplines = Service.FindDisciplineResponse(filter);

            SetDataToViewBag(user);
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
            var disciplines = Service.FindDisciplineResponse(SearchFilter<Discipline>.FilterByIds(user.Disciplines.Select(el => el.Id)));
            return View(disciplines);
        }

        public ActionResult Register(string id)
        {
            var result = Service.TryRegisterStudent(UserId, id);
            ViewBag.RegisterResult = result ? "Успішно зареєстровано" : "Упс, щось не так";
            return View();
        }

        public ActionResult Unregister(string id)
        {
            try
            {
                Service.UnregisterStudent(UserId, id);
                return View();
            }
            catch
            {
                throw;
            }
        }


        #region Helpers
        private void SetDataToViewBag(User user)
        {
            ViewBag.User = user;
            ViewBag.UserDisciplines = user.Disciplines != null && user.Disciplines.Any()
                ? Service.Find(SearchFilter<Discipline>.FilterByIds(user.Disciplines.Select(el => el.Id)))
                : new List<Discipline>();

            //var settings = ServiceFactory.SettingService.Find(SearchFilter<Setting>.Empty);
            //ViewBag.Start = settings.SingleOrDefault(s => s.Name == Constants.StartDate);
            //ViewBag.First = settings.SingleOrDefault(s => s.Name == Constants.FirstDeadline);
            //ViewBag.Second = settings.SingleOrDefault(s => s.Name == Constants.SecondDeadline);
        }
        #endregion
    }
}
