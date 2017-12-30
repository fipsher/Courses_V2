using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Courses_v2.Models;
using Core.Interfaces.Services;
using System.Collections.Generic;
using static Core.Enums.Enums;
using Core.Entities;
using System;
using Core.Helpers;
using System.Linq;
using System.Net.Http;
using System.Data.OleDb;
using System.Data;
using System.Web.Hosting;

namespace Courses_v2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService _userService;
        private readonly IDisciplineService discService;
        private readonly IGroupService groupService;
        private readonly ICathedraService cathedraService;

        public AccountController(
            IUserService userService,
            IDisciplineService discService,
            IGroupService groupService,
            ICathedraService cathedraService
            )
        {
            _userService = userService;
            this.discService = discService;
            this.groupService = groupService;
            this.cathedraService = cathedraService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public async Task Wave(int wave)
        {
            using (var client = new HttpClient())
            {
                await client.GetAsync($"http://localhost:2000/api/scheduler/test/schedule-wave/{wave}");
            }
        }

        [AllowAnonymous]
        public void Seed()
        {
            _userService.Add(new User()
            {
                Login = $"AndrewThe",
                Password = "Ryba5656.",
                Email = "fipsher@gmail.com",
                Roles = new List<Role> { Role.Admin },
                UserName = "AnderewThe",
                GroupId = "29627000-e32d-11e7-b070-a7a2334df747",
                PhoneNumber = "123-123-123"
            });
            var fileName = $@"{HostingEnvironment.ApplicationPhysicalPath}\students.xls";
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", fileName);

            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "anyNameHere");

            DataTable data = ds.Tables["anyNameHere"];

            List<Group> groupExist = new List<Group>();
            List<Cathedra> facultyExist = new List<Cathedra>();

            int i = 0;
            foreach (DataRow row in data.Rows)
            {
                Group usrGrp;
                var student = new Student
                {
                    LastName = row[0].ToString(),
                    FirstName = row[1].ToString(),
                    ParentName = row[2].ToString(),
                    GroupName = row[3].ToString(),
                    Faculty = row[4].ToString(),
                    Course = row[5].ToString()
                };

                if (facultyExist.All(el => el.Name != student.Faculty))
                {
                    var cathedra = new Cathedra { Id = Guid.NewGuid().ToString(), Name = student.Faculty };
                    cathedraService.Add(cathedra);
                    facultyExist.Add(cathedra);
                }

                usrGrp = groupExist.SingleOrDefault(el => el.Name == student.GroupName);
                if (usrGrp == null)
                {                    
                    usrGrp = new Group
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = student.GroupName,
                        CathedraId = facultyExist.SingleOrDefault(c => c.Name == student.Faculty).Id,
                        Course = Convert.ToInt32(student.Course),
                        DisciplineConfiguration = new List<DisciplineConfiguration>
                        {
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester = 1},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester =  2},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 1},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 2}
                        },
                    };
                    groupService.Add(usrGrp);
                    groupExist.Add(usrGrp);
                }

                var studentUser = new User
                {
                    Roles = new List<Role> { Role.Student },
                    Course = usrGrp.Course,
                    UserName = $"{student.LastName} {student.FirstName} {student.ParentName}",
                    Login = $"student{i++}",
                    Password = "Ryba5656.",
                    GroupId = usrGrp.Id
                };

                _userService.Add(studentUser);
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}