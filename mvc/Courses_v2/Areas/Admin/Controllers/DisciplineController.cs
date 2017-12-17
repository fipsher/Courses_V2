using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Moderator, Admin, Lecturer")]
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        public DisciplineController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.DisciplineService)
        {
        }

        // GET: Moderator/Disciplines
        public ActionResult Index()
        {
            var filter = SearchFilter<Discipline>.Default;
            if (User.IsInRole("Lecturer"))
            {
                filter.OptionList = new List<Discipline>
                {
                     new Discipline{LecturerId = UserId}
                };
            }

            var disciplines = Service.FindDisciplineResponse(filter);
            return View(disciplines);
        }
        // Post: Moderator/Disciplines
        [HttpPost]
        public ActionResult Index(Discipline filter = null)
        {
            filter = filter ?? new Discipline();
            var searchFilter = SearchFilter<Discipline>.Default;
            if (User.IsInRole("Lecturer"))
            {
                filter.LecturerId = UserId;
            }
            searchFilter.OptionList = FilterHelper.OptionListByEntity<Discipline>(filter);

            var disciplines = Service.FindDisciplineResponse(searchFilter);
            return View(disciplines);
        }

        // GET: Moderator/Disciplines/Details/5
        public ActionResult Details(string id)
        {
            var disciplines = Service.FindDisciplineResponse((new SearchFilter<Discipline>
            {
                OptionList = new[] { new Discipline() { Id = id } }
            })).SingleOrDefault();

            return View(disciplines);
        }

        // GET: Moderator/Disciplines/Create
        [Authorize(Roles = "Moderator, Admin")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        // POST: Moderator/Disciplines/Create
        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
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
                throw;
            }
            SetViewBag();
            return View();

        }

        // GET: Moderator/Disciplines/Edit/5
        public ActionResult Edit(string id)
        {
            var discipline = Service.Find((new SearchFilter<Discipline>() { OptionList = new[] { new Discipline() { Id = id } } }))
                                     .SingleOrDefault();
            if (User.IsInRole("Lecturer") && discipline.LecturerId != UserId)
            {
                throw new Exception("Not allowed");
            }
            SetViewBag();
            return View(discipline);
        }
        // POST: Moderator/Disciplines/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Discipline discipline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingDescipline = Service.Find((new SearchFilter<Discipline>() { OptionList = new[] { new Discipline() { Id = id } } }))
                         .SingleOrDefault();
                    if (User.IsInRole("Lecturer") && existingDescipline.LecturerId != UserId)
                    {
                        throw new Exception("Not allowed");
                    }
                    Service.Update(id, discipline);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            SetViewBag();
            return View();
        }

        // GET: Moderator/Disciplines/Delete/5
        [Authorize(Roles = "Moderator, Admin")]
        public ActionResult Delete(string id)
        {
            var disciplines = Service.FindDisciplineResponse((new SearchFilter<Discipline>()
            {
                OptionList = new[] { new Discipline() { Id = id } }
            }));

            return View(disciplines?.SingleOrDefault());
        }
        // POST: Moderator/Disciplines/Delete/5
        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public ActionResult Delete(string id, Discipline discipline)
        {
            try
            {
                Service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }

        private void SetViewBag()
        {
            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty)
                                    .Select(x => new SelectListItem
                                    {
                                        Text = x.Name,
                                        Value = x.Id
                                    }); ;

            var lecturersFilter = new SearchFilter<User>
            {
                OptionList = FilterHelper.LecturerOptionList
            };
            ViewBag.Lecturers = ServiceFactory.UserService.Find(lecturersFilter)
                                            .Select(x => new SelectListItem
                                            {
                                                Text = x.UserName,
                                                Value = x.Id
                                            });
        }
    }
}
