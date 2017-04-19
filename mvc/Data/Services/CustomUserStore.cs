using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace Data.Services
{
    public class CustomUserStore : IUserStore<User>, IUserLockoutStore<User, string>, IUserPasswordStore<User>,
                                   IUserTwoFactorStore<User, string>, IUserPhoneNumberStore<User>, IUserSecurityStampStore<User>, IUserEmailStore<User>
    {
        public IUserRepository Repository { get; private set; }

        public CustomUserStore(IUserRepository userRepository)
        {
            Repository = userRepository;
        }


        //IUserStore
        public Task CreateAsync(User user) => Task.Run(() => Repository.Add(user));
        public Task DeleteAsync(User user) => Task.Run(() => Repository.Delete(user.Id)); 
        public Task<User> FindByIdAsync(string userId) => Task.FromResult(Repository.Find(userId));
        public Task<User> FindByNameAsync(string userName) => FindByIdAsync(userName);
        public Task UpdateAsync(User user) => Task.Run(() => Repository.Update(user));
        public void Dispose() { }

        //IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user) => throw new NotImplementedException();
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd) => Task.FromResult(0);
        public Task<int> IncrementAccessFailedCountAsync(User user) => Task.FromResult(0);
        public Task ResetAccessFailedCountAsync(User user) => Task.FromResult(0);
        public Task<int> GetAccessFailedCountAsync(User user) => Task.FromResult(0);
        public Task<bool> GetLockoutEnabledAsync(User user) => Task.FromResult(false);
        public Task SetLockoutEnabledAsync(User user, bool enabled) => Task.FromResult(0);

        //IUserPasswordStore
        public Task SetPasswordHashAsync(User user, string passwordHash) => Task.Run(() => 
        {
            user.Password = passwordHash;
            Repository.Update(user);
        });
        public Task<string> GetPasswordHashAsync(User user) => Task.FromResult(user.Password);
        public Task<bool> HasPasswordAsync(User user) => Task.FromResult(!string.IsNullOrEmpty(user.Password));

        //IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(User user, bool enabled) => throw new NotImplementedException();
        public Task<bool> GetTwoFactorEnabledAsync(User user) => Task.FromResult(false);

        //IUserPhoneNumberStore
        public Task SetPhoneNumberAsync(User user, string phoneNumber) => Task.Run(() =>
        {
            user.PhoneNumber = phoneNumber;
            Repository.Update(user);
        });
        public Task<string> GetPhoneNumberAsync(User user) => Task.FromResult(user.PhoneNumber);
        public Task<bool> GetPhoneNumberConfirmedAsync(User user) => Task.FromResult(true);
        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed) => Task.FromResult(0);

        //IUserSecurityStampStore
        public Task SetSecurityStampAsync(User user, string stamp) => Task.FromResult(0);
        public Task<string> GetSecurityStampAsync(User user) => Task.FromResult(string.Empty);

        //IUserEmailStore
        public Task SetEmailAsync(User user, string email) => Task.Run(() =>
        {
            user.Email = email;
            Repository.Update(user);
        });
        public Task<string> GetEmailAsync(User user) => Task.FromResult(user.Email);
        public Task<bool> GetEmailConfirmedAsync(User user) => Task.FromResult(true);
        public Task SetEmailConfirmedAsync(User user, bool confirmed) => Task.FromResult(0);
        public Task<User> FindByEmailAsync(string email) => Task.FromResult(Repository.GetByEmail(email));
    }
}
