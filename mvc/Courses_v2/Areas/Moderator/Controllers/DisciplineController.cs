﻿using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Moderator.Controllers
{
    // will be added some base discipline ctrl
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        public DisciplineController(IDisciplineService _disciplineService) : base(_disciplineService)
        {
            Service = _disciplineService;
        }

        // GET: Admin/Disciplines
        public ActionResult Index(SearchFilter<Discipline> filter = null)
        {
            filter = filter ?? SearchFilter<Discipline>.Default;
            var disciplines = Service.FindDisciplineResponse(filter);
            return View(disciplines);
        }

        // GET: Admin/Disciplines/Details/5
        public ActionResult Details(string id)
        {
            var discipline = Service.Find((new SearchFilter<Discipline>
            {
                Query = new[] { new Discipline() { Id = id } }
            })).SingleOrDefault();
            if (CheckLecturerAccess(discipline))
            {
                return View(discipline);
            }
            return RedirectToAction("Index");// is better to redirect to Access denied 
        }

        // GET: Admin/Disciplines/Edit/5
        public ActionResult Edit(string id)
        {
            // same logic
            var discipline = Service.Find(new SearchFilter<Discipline>()
            {
                Query = new[] { new Discipline() { Id = id } }
            }).SingleOrDefault();
            if (CheckLecturerAccess(discipline))
            {
                return View(discipline);
            }
            return RedirectToAction("Index");// is better to redirect to Access denied 
        }
        // POST: Admin/Disciplines/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Discipline discipline)
        {
            try
            {
                // lect logic
                if (ModelState.IsValid)
                {
                    if (CheckLecturerAccess(discipline))
                    {
                        Service.Add(discipline);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }

        #region Helpers
        private bool CheckLecturerAccess(Discipline discipline)
        {
            return discipline.LecturerId == UserId;
        }
        #endregion
    }
}