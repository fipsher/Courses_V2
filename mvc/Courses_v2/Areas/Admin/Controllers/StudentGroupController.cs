using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class StudentGroupController : BaseController<StudentGroup, IStudentGroupService>
    {
        public StudentGroupController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.StudentGroupService)
        {
        }

        // GET: Admin/StudentGroup
        public ActionResult Index()
        {
            var users = Service.Find(SearchFilter<StudentGroup>.Default);
            return View(users);
        }
        // Post: Admin/StudentGroup
        [HttpPost]
        public ActionResult Index(StudentGroup filter = null)
        {
            filter = filter ?? new StudentGroup();
            var searchFilter = SearchFilter<StudentGroup>.Default;

            searchFilter.OptionList = FilterHelper.OptionListByEntity<StudentGroup>(filter);

            var users = Service.Find(searchFilter);
            return View(users);
        }

        // GET: Admin/StudentGroup/Details/5
        public ActionResult Details(string id)
        {
            var groups = Service.Find((new SearchFilter<StudentGroup>
            {
                OptionList = new[] { new StudentGroup() { Id = id } }
            }));

            return View(groups.SingleOrDefault());
        }

        // GET: Admin/StudentGroup/Create
        public ActionResult Create() => View();

        // POST: Admin/StudentGroup/Create
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

        // GET: Admin/StudentGroup/Edit/5
        public ActionResult Edit(string id)
        {
            var groups = Service.Find((new SearchFilter<StudentGroup>() { OptionList = new[] { new StudentGroup() { Id = id } } }));

            return View(groups.SingleOrDefault());
        }

        // POST: Admin/StudentGroup/Edit/5
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

        // GET: Admin/StudentGroup/Delete/5
        public ActionResult Delete(string id)
        {
            var groups = Service.Find((new SearchFilter<StudentGroup>()
            {
                OptionList = new[] { new StudentGroup() { Id = id } }
            }));

            return View(groups?.SingleOrDefault());
        }

        // POST: Admin/StudentGroup/Delete/5
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
