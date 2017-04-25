using Core.Entities;
using Core.Helpers;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class DisciplineController : BaseController<Discipline, IDisciplineService>
    {
        public DisciplineController(IDisciplineService _disciplineService) : base(_disciplineService)
        {
            Service = _disciplineService;
        }

        // GET: Admin/Disciplines
        public ActionResult Index(int skip = 0, int take = 100, string nameFilter = "")
        {
            var disciplines = Service.FindDisciplineResponse((new ExtendedSearchFilter<Discipline>
            {
                Take = take,
                Skip = skip,
                Query = new[] { new Discipline { Name = nameFilter } }
            }));
            return View(disciplines);
        }
        
        // GET: Admin/Disciplines/Details/5
        public ActionResult Details(string id)
        {
            var disciplines = Service.Find((new BaseSearchFilter<Discipline>
            {
                Query = new[] { new Discipline() { Id = id } }
            }));

            return View(disciplines.SingleOrDefault());
        }
       
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
            var disciplines = Service.Find((new BaseSearchFilter<Discipline>() { Query = new[] { new Discipline() { Id = id } } }));

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
            var disciplines = Service.Find((new BaseSearchFilter<Discipline>()
            {
                Query = new[] { new Discipline() { Id = id } }
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
