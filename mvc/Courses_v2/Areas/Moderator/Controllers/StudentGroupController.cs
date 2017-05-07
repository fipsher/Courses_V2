using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Moderator.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class StudentGroupController : BaseController<StudentGroup, IStudentGroupService>
    {
        public StudentGroupController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.StudentGroupService)
        {
        }

        // GET: Moderator/StudentGroup
        public ActionResult Index(SearchFilter<StudentGroup> filter = null)
        {
            filter = filter == null || filter.OptionList == null ? SearchFilter<StudentGroup>.Default : filter;
            var groups = Service.Find(filter);
            return View(groups);
        }

        // GET: Moderator/StudentGroup/Details/5
        public ActionResult Details(string id)
        {
            var groups = Service.Find((new SearchFilter<StudentGroup>
            {
                OptionList = new[] { new StudentGroup() { Id = id } }
            }));

            return View(groups.SingleOrDefault());
        }

        // GET: Moderator/StudentGroup/Create
        public ActionResult Create() => View();

        // POST: Moderator/StudentGroup/Create
        [HttpPost]
        public ActionResult Create(StudentGroup studentGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Add(studentGroup);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("InternalServer", "Error", new { area = "" });
            }
            return View();
        }

        // GET: Moderator/StudentGroup/Edit/5
        public ActionResult Edit(string id)
        {
            var groups = Service.Find((new SearchFilter<StudentGroup>() { OptionList = new[] { new StudentGroup() { Id = id } } }));

            return View(groups.SingleOrDefault());
        }

        // POST: Moderator/StudentGroup/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, StudentGroup studentGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Update(id, studentGroup);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("InternalServer", "Error", new { area = "" });
            }
            return View();
        }

        // GET: Moderator/StudentGroup/Delete/5
        public ActionResult Delete(string id)
        {
            var groups = Service.Find((new SearchFilter<StudentGroup>()
            {
                OptionList = new[] { new StudentGroup() { Id = id } }
            }));

            return View(groups?.SingleOrDefault());
        }

        // POST: Moderator/StudentGroup/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, StudentGroup studentGroup)
        {
            try
            {
                Service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("InternalServer", "Error", new { area = "" });
            }
        }
    }
}
