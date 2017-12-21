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

namespace Courses_v2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService _userService;
        private readonly IDisciplineService d;
        private readonly IGroupService g;
        private readonly ICathedraService c;

        public AccountController(
            IUserService userService,
            IDisciplineService d,
            IGroupService g,
            ICathedraService c
            )
        {
            _userService = userService;
            this.d = d;
            this.g = g;
            this.c = c;
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
        public void Seed()
        {
            _userService.Add(new Core.Entities.User()
            {
                Login = $"AndrewThe",
                Password = "Ryba5656.",
                Email = "fipsher@gmail.com",
                Roles = new List<Role> { Role.Admin },
                UserName = "AnderewThe",
                GroupId = "29627000-e32d-11e7-b070-a7a2334df747",
                PhoneNumber = "123-123-123"
            });
            var lecturer = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Login = $"Lecturer",
                Password = "Ryba5656.",
                Email = "fipsher@gmail.com",
                Roles = new List<Role> { Role.Lecturer },
                UserName = "AnderewThe",
                GroupId = "29627000-e32d-11e7-b070-a7a2334df747",
                PhoneNumber = "123-123-123"
            };
            _userService.Add(lecturer);

            var cathedra = new Cathedra
            {
                Id = Guid.NewGuid().ToString(),
                Name = "App Math"
            };

            c.Add(cathedra);
            var group = new Group()
            {
                Id = Guid.NewGuid().ToString(),
                CathedraId = cathedra.Id,
                Name = "PMP-51",
                DisciplineConfiguration = new List<DisciplineConfiguration>
                {
                    new DisciplineConfiguration { DisciplineType =  DisciplineType.Socio, RequiredAmount = 1, Semester = 1 },
                    new DisciplineConfiguration { DisciplineType =  DisciplineType.Socio, RequiredAmount = 1, Semester = 2 },
                    new DisciplineConfiguration { DisciplineType =  DisciplineType.Special, RequiredAmount = 1, Semester = 1 },
                    new DisciplineConfiguration { DisciplineType =  DisciplineType.Special, RequiredAmount = 1, Semester = 2 },
                },
                Course = 5,
            };
            g.Add(group);

            for (int i = 0; i < 99; i++)
            {
                _userService.Add(new User()
                {
                    Login = $"student{i}",
                    Password = "Ryba5656.",
                    Email = $"fipsher@gmail.com",
                    Roles = new List<Role> { Role.Student },
                    UserName = $"student{i}",
                    Course = 5,
                    GroupId = group.Id,
                    PhoneNumber = "123-123-123",
                });

                d.Add(new Discipline
                {
                    Id = Guid.NewGuid().ToString(),
                    DisciplineType = i % 2 == 0 ? DisciplineType.Socio : DisciplineType.Special,
                    IsAvailable = true,
                    LecturerId = lecturer.Id,
                    Name = $"Discipline{i}_11",
                    Semester = 11,
                    ProviderCathedraId = cathedra.Id
                });
                var di = new Discipline
                {
                    Id = Guid.NewGuid().ToString(),
                    DisciplineType = i % 2 == 0 ? DisciplineType.Socio : DisciplineType.Special,
                    IsAvailable = true,
                    LecturerId = lecturer.Id,
                    Name = $"Discipline{i}_12",
                    Semester = 12,
                    ProviderCathedraId = cathedra.Id
                };
                d.Add(di);
            }
            var disciplines = d.Find(SearchFilter<Discipline>.FilterByEntity(new Discipline { DisciplineType = DisciplineType.Special }));
            disciplines = new List<Discipline> { disciplines[0], disciplines[1] };
            group.DisciplineSubscriptions = disciplines.Select(d => d.Id).ToList();

            g.Update(group.Id, group);
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