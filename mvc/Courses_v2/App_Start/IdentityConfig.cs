using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Core.Entities;
using Data.Services;
using Courses_v2.App_Start;
using Ninject;
using Core.Interfaces.Services;
using Courses_v2.Models;
using Courses_v2.Authentication;

namespace Courses_v2
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) => password;
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword) => hashedPassword == providedPassword ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
        public async Task UpdatePhoneNumber(string userId, string phoneNumber)
        {
            var user = await Store.FindByIdAsync(userId);
            user.PhoneNumber = phoneNumber;
            await Store.UpdateAsync(user);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var userService = NinjectWebCommon.Bootstrapper.Kernel.Get<IUserService>();
            var manager = new ApplicationUserManager(new CustomUserStore(userService))
            {
                PasswordHasher = new PasswordHasher()
            };
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        //public override Task<SignInStatus> PasswordSignInAsync(string usApplicationUsererName, string password, bool isPersistent, bool shouldLockout)
        //{
        //    return Task.FromResult(SignInStatus.Success);
        //}

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user) => user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) => new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
}
