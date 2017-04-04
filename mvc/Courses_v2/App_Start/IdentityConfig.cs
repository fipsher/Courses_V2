using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Core.Entities;
using Data.Services;

namespace Courses_v2
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) => password;
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword) => hashedPassword == providedPassword ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }

    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var ds = new DataService();
            var manager = new ApplicationUserManager(new CustomUserStore(ds.Users));
            //var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));
            manager.PasswordHasher = new PasswordHasher();
            manager.UserValidator = new UserValidator<User>(manager)
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
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        //public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        //{
        //    return Task.FromResult(SignInStatus.Success);
        //}

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user) => user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) => new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }
}
