using Core.Entities;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;
using System.Linq;
using Core.Interfaces.Services;
using Courses_v2.Models;

namespace Courses_v2.Authentication
{
    public class CustomUserStore : IUserStore<ApplicationUser>, IUserLockoutStore<ApplicationUser, string>, IUserPasswordStore<ApplicationUser>,
                                   IUserTwoFactorStore<ApplicationUser, string>, IUserPhoneNumberStore<ApplicationUser>, IUserSecurityStampStore<ApplicationUser>, IUserEmailStore<ApplicationUser>
    {
        private IUserService Service { get; set; }

        public CustomUserStore(IUserService userRepository)
        {
            Service = userRepository;
        }


        //IUserStore
        public Task CreateAsync(ApplicationUser user) => Task.Run(() => Service.Add(user));
        public Task DeleteAsync(ApplicationUser user) => Task.Run(() => Service.Delete(user.Id));
        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            var user = Service.Find(new Core.Helpers.SearchFilter<User>
            {
                OptionList = new[] { new User { Email = userId } }
            }).SingleOrDefault();

            return Task.FromResult((ApplicationUser)user);
        }
        public Task<ApplicationUser> FindByNameAsync(string userName) => FindByIdAsync(userName);
        public Task UpdateAsync(ApplicationUser user) => Task.Run(() => Service.Update(user.Id, user));
        public void Dispose() { }

        //IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        public Task SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset lockoutEnd) => Task.FromResult(0);
        public Task<int> IncrementAccessFailedCountAsync(ApplicationUser user) => Task.FromResult(0);
        public Task ResetAccessFailedCountAsync(ApplicationUser user) => Task.FromResult(0);
        public Task<int> GetAccessFailedCountAsync(ApplicationUser user) => Task.FromResult(0);
        public Task<bool> GetLockoutEnabledAsync(ApplicationUser user) => Task.FromResult(false);
        public Task SetLockoutEnabledAsync(ApplicationUser user, bool enabled) => Task.FromResult(0);

        //IUserPasswordStore
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash) => Task.Run(() =>
        {
            user.Password = passwordHash;
            Service.Update(user.Id, user);
        });
        public Task<string> GetPasswordHashAsync(ApplicationUser user) => Task.FromResult(user.Password);
        public Task<bool> HasPasswordAsync(ApplicationUser user) => Task.FromResult(!string.IsNullOrEmpty(user.Password));

        //IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }
        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user) => Task.FromResult(false);

        //IUserPhoneNumberStore
        public Task SetPhoneNumberAsync(ApplicationUser user, string phoneNumber) => Task.Run(() =>
        {
            user.PhoneNumber = phoneNumber;
            Service.Update(user.Id, user);
        });
        public Task<string> GetPhoneNumberAsync(ApplicationUser user) => Task.FromResult(user.PhoneNumber);
        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUser user) => Task.FromResult(true);
        public Task SetPhoneNumberConfirmedAsync(ApplicationUser user, bool confirmed) => Task.FromResult(0);

        //IUserSecurityStampStore
        public Task SetSecurityStampAsync(ApplicationUser user, string stamp) => Task.FromResult(0);
        public Task<string> GetSecurityStampAsync(ApplicationUser user) => Task.FromResult(string.Empty);

        //IUserEmailStore
        public Task SetEmailAsync(ApplicationUser user, string email) => Task.Run(() =>
        {
            user.Email = email;
            Service.Update(user.Id, user);
        });
        public Task<string> GetEmailAsync(ApplicationUser user) => Task.FromResult(user.Email);
        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user) => Task.FromResult(true);
        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed) => Task.FromResult(0);
        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = Service.Find(new Core.Helpers.SearchFilter<User>
            {
                OptionList = new[] { new User { Email = email } }
            }).SingleOrDefault();
            return Task.FromResult((ApplicationUser)user);
        }
    }
}
