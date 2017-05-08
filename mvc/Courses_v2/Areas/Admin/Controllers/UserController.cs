using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Courses_v2.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController<User, IUserService>
    {
        public UserController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.UserService)
        {
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            var users = Service.Find(SearchFilter<User>.Default);
            return View(users);
        }
        // Post: Admin/User
        [HttpPost]
        public ActionResult Index(User filter = null)
        {
            filter = filter ?? new User();
            var searchFilter = SearchFilter<User>.Default;

            searchFilter.OptionList = FilterHelper.OptionListByEntity<User>(filter);

            var users = Service.Find(searchFilter);
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
        public ActionResult Create()
        {
            ViewBag.StudentGroups = ServiceFactory.GroupService.Find(SearchFilter<Group>.Empty);

            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Service.TryAdd(user))
                    {
                        return RedirectToAction("Index");
                    }
                    ViewBag.LoginExists = "Юзер з таким логіном вже існує";
                }
            }
            catch
            {
                throw;
            }
            ViewBag.StudentGroups = ServiceFactory.GroupService.Find(SearchFilter<Group>.Empty);

            return View();
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.StudentGroups = ServiceFactory.GroupService.Find(SearchFilter<Group>.Empty);

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
                    Service.Update(id, user);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                throw;
            }
            var users = Service.Find((new SearchFilter<User>() { OptionList = new[] { new User() { Id = id } } }));

            return View(users.SingleOrDefault());
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
                throw;
            }
        }
    }
}