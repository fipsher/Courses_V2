using Core;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Responces;
using Courses_v2.Areas.Student.Models;
using Courses_v2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Core.Enums.Enums;

namespace Courses_v2.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        private readonly IGroupService _groupService;
        private readonly ISettingService _settingsSerivice;

        public DisciplineController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.DisciplineService)
        {
            _groupService = serviceFactory.GroupService;
            _settingsSerivice = serviceFactory.SettingService;
        }

        public ActionResult SocioDisciplines()
        {
            var model = GetDisciplines(DisciplineType.Socio);

            return View("Index", model);
        }

        public ActionResult SpecialDisciplines()
        {
            var model = GetDisciplines(DisciplineType.Special);

            return View("Index", model);
        }

        public StudentDisciplines GetDisciplines(DisciplineType type)
        {
            var user = ServiceFactory.UserService.Find(SearchFilter<User>.FilterById(UserId)).SingleOrDefault();
            var group = _groupService.Find(SearchFilter<Group>.FilterById(user.GroupId)).SingleOrDefault();

            var filter = SearchFilter<Discipline>.Empty;
            filter.OptionList = type == DisciplineType.Socio
                ? FilterHelper.SocialDisciplines(user.Course.Value, true)
                : FilterHelper.SpecialDisciplines(user.Course.Value, true);

            var disciplines = Service.FindDisciplineResponse(filter);
            var userDisciplines = user.Disciplines != null && user.Disciplines.Any()
                ? Service.Find(SearchFilter<Discipline>.FilterByIds(user.Disciplines.Select(el => el.Id)))
                : new List<Discipline>();
            var deadlines = _settingsSerivice.Find(SearchFilter<Setting>.Empty);
            var settings = group.DisciplineConfiguration;

            DisciplineConfiguration firstSemester = settings.SingleOrDefault(el => el.Semester % 2 == 1 && el.DisciplineType == type);
            DisciplineConfiguration secondSemester = settings.SingleOrDefault(el => el.Semester % 2 == 0 && el.DisciplineType == type);
            List<Discipline> userSocioDisciplines = userDisciplines.Where(ud => ud.DisciplineType == type).ToList();
            var start = deadlines.SingleOrDefault(el => el.Name == Constants.StartDate)?.Value;
            var first = deadlines.SingleOrDefault(el => el.Name == Constants.FirstDeadline)?.Value;
            var second = deadlines.SingleOrDefault(el => el.Name == Constants.SecondDeadline)?.Value;

            bool checkTime = CheckTime(start, first, second);
            disciplines.ForEach(d =>
            {
                d.IsLocked = user.Disciplines.Any(di => di.Id == d.Id && di.Locked);
                d.Registered = user.Disciplines.Any(di => di.Id == d.Id);
                d.CanRegister = CanRegister(d, firstSemester, secondSemester, userSocioDisciplines) && checkTime;
            });

            return new StudentDisciplines
            {
                Disciplines = disciplines,
                Type = DisciplineType.Socio,
            };
        }

        public ActionResult Details(string id)
        {
            var discipline = Service.FindDisciplineResponse((new SearchFilter<Discipline>
            {
                OptionList = new[] { new Discipline() { Id = id } }
            })).SingleOrDefault();

            return View(discipline);
        }

        public ActionResult MyDisciplines()
        {
            var user = ServiceFactory.UserService.Find(SearchFilter<User>.FilterById(UserId)).SingleOrDefault();
            var disciplines = user.Disciplines.Any()
                ? Service.FindDisciplineResponse(SearchFilter<Discipline>.FilterByIds(user.Disciplines.Select(el => el.Id)))
                : new List<GroupDisciplineModel>();
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

        public bool CheckTime( DateTime? start, DateTime? first, DateTime? second)
        {
            DateTime now = DateTime.Now;
            if (start.HasValue)
            {
                now.AddYears(start.HasValue ? start.Value.Year - now.Year : now.Year);
            }
            return start.HasValue && first.HasValue && second.HasValue
            && (
                start.Value < now && first.Value > now
                ||
                first.Value.AddHours(1) < now && second.Value > now
            );
        }

        public bool CanRegister(
            GroupDisciplineModel item,
            DisciplineConfiguration firstSemester,
            DisciplineConfiguration secondSemester,
            List<Discipline> userSocioDisciplines)
        {
            bool result = ((item.Semester % 2 == 1
                           && firstSemester != null
                           && userSocioDisciplines.Count(el => el.Semester == item.Semester) < firstSemester.RequiredAmount
                           && userSocioDisciplines.All(usd => usd.Id != item.Id))
                       ||
                       (item.Semester % 2 == 0
                           && secondSemester != null
                           && userSocioDisciplines.Count(el => el.Semester == item.Semester) < secondSemester.RequiredAmount
                           && userSocioDisciplines.All(usd => usd.Id != item.Id)))
                        &&
                        item.Count < Constants.DisciplineMaxCapacity;

            return result;
        }
        #endregion
    }
}
