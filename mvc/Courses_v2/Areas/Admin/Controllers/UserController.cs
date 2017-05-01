using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class UserController : BaseController<User, IUserService>
    {
        public UserController(IServiceFactory serviceFactory) : base(serviceFactory.UserService)
        {
        }

        // GET: Admin/User
        [HttpPost]
        public ActionResult Index(SearchFilter<User> filter = null)
        {
            filter = filter ?? SearchFilter<User>.Default;
            var users = Service.Find(filter);
            return View(users);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(string id)
        {
            var users = Service.Find((new SearchFilter<User>
            {
                OptionList = new[] { new User() { Id = id } }
            }));

            return View(users.SingleOrDefault());
        }

        // GET: Admin/User/Create
        public ActionResult Create() => View();

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Add(user);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(string id)
        {
            var users = Service.Find((new SearchFilter<User>() { OptionList = new[] { new User() { Id = id } } }));

            return View(users.SingleOrDefault());
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Update(user);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(string id)
        {
            var users = Service.Find((new SearchFilter<User>()
            {
                OptionList = new[] { new User() { Id = id } }
            }));

            return View(users?.SingleOrDefault());
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, User user)
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
