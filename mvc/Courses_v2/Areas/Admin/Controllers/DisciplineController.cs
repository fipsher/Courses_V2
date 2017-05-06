using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Core.Enums.Enums;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        public DisciplineController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.DisciplineService)
        {
        }

        // GET: Admin/Disciplines
        public ActionResult Index(SearchFilter<Discipline> filter = null)
        {
            filter = filter == null || filter.OptionList == null ? SearchFilter<Discipline>.Empty : filter;
            var disciplines = Service.FindDisciplineResponse(filter);
            return View(disciplines);
        }

        // GET: Admin/Disciplines/Details/5
        public ActionResult Details(string id)
        {
            var disciplines = Service.FindDisciplineResponse((new SearchFilter<Discipline>
            {
                OptionList = new[] { new Discipline() { Id = id } }
            }));

            return View(disciplines.SingleOrDefault());
        }

        // GET: Admin/Disciplines/Create
        public ActionResult Create()
        {
            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty)
                                                .Select(x => new SelectListItem
                                                {
                                                    Text = x.Name,
                                                    Value = x.Id
                                                }); ;

            var lecturersFilter = new SearchFilter<User>
                                        {
                                            OptionList = new List<User>()
                                            {
                                                new User(){Roles = new List<Role> { Role.Lecturer }}
                                            }
                                        };
            ViewBag.Lecturers = ServiceFactory.UserService.Find(lecturersFilter)
                                            .Select(x => new SelectListItem
                                            {
                                                Text = x.UserName,
                                                Value = x.Id
                                            });

            return View();
        }
        // POST: Admin/Disciplines/Create
        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Add(discipline);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();

        }

        // GET: Admin/Disciplines/Edit/5
        public ActionResult Edit(string id)
        {
            var disciplines = Service.Find((new SearchFilter<Discipline>() { OptionList = new[] { new Discipline() { Id = id } } }));

            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty)
                                                .Select(x => new SelectListItem
                                                {
                                                    Text = x.Name, Value = x.Id
                                                }); ;

            var lecturersFilter = new SearchFilter<User>
            {
                OptionList = FilterHelper.LecturerOptionList
            };
            ViewBag.Lecturers = ServiceFactory.UserService.Find(lecturersFilter)
                                            .Select(x => new SelectListItem
                                            {
                                                Text = x.UserName, Value = x.Id
                                            });

            return View(disciplines.SingleOrDefault());
        }
        // POST: Admin/Disciplines/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Discipline discipline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Add(discipline);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }

        // GET: Admin/Disciplines/Delete/5
        public ActionResult Delete(string id)
        {
            var disciplines = Service.FindDisciplineResponse((new SearchFilter<Discipline>()
            {
                OptionList = new[] { new Discipline() { Id = id } }
            }));

            return View(disciplines?.SingleOrDefault());
        }
        // POST: Admin/Disciplines/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Discipline discipline)
        {
            try
            {
                Service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
