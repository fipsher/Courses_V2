using Core.Entities;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class StudentGroupController : BaseController<StudentGroup, IStudentGroupService>
    {
        public StudentGroupController(IStudentGroupService service) : base(service)
        {
        }

        // GET: Admin/Group
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Group/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Group/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Group/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Group/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Group/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Group/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
