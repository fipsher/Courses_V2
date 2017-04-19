using Core;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class DisciplineController : Controller
    {
        private IDisciplineRepository _disciplineRepository;
        public DisciplineController(IDisciplineRepository disciplineRepository)
        {
            _disciplineRepository = disciplineRepository;
        }

        // GET: Admin/Disciplines
        public ActionResult Index(int skip = 0, string nameFilter = "") => View(_disciplineRepository.Get(Strings.DefaultTake, skip, nameFilter));

        // GET: Admin/Disciplines/Details/5
        public ActionResult Details(Guid id) => View(_disciplineRepository.Find(id));

        // GET: Admin/Disciplines/Create
        public ActionResult Create() => View();

        // POST: Admin/Disciplines/Create
        [HttpPost]
        public ActionResult Create(Discipline discipline)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _disciplineRepository.Add(discipline);
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
        public ActionResult Edit(Guid id) => View(_disciplineRepository.Find(id));

        // POST: Admin/Disciplines/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Discipline discipline)
        {
            try
            {
                _disciplineRepository.Update(discipline);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Disciplines/Delete/5
        public ActionResult Delete(Guid id) => View(_disciplineRepository.Find(id));

        // POST: Admin/Disciplines/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Discipline discipline)
        {
            try
            {
                // TODO: Add delete logic here
                _disciplineRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
