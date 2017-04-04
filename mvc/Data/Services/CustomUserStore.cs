using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace Data.Services
{
    public class CustomUserStore : IUserStore<User>, IUserLockoutStore<User, string>, IUserPasswordStore<User>, IUserTwoFactorStore<User, string>
    {
        public IRepository<User, string> Reposirory { get; private set; }

        public CustomUserStore(IRepository<User, string> userRepository)
        {
            Reposirory = userRepository;
        }



        public Task CreateAsync(User user) => Task.Run(() => Reposirory.Add(user));
        public Task DeleteAsync(User user) => Task.Run(() => Reposirory.Delete(user.Id));
        public Task<User> FindByIdAsync(string userId) => Task.FromResult(Reposirory.Find(userId));
        public Task<User> FindByNameAsync(string userName) => Task.FromResult(Reposirory.Find(userName));
        public Task UpdateAsync(User user) => Task.Run(() => Reposirory.Update(user));
        public void Dispose() { }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user) => Task.FromResult(new DateTimeOffset(user.LockoutEndDateUtc.Value));
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd) => Task.FromResult(user.LockoutEndDateUtc = lockoutEnd.UtcDateTime);
        public Task<int> IncrementAccessFailedCountAsync(User user) => Task.FromResult(user.AccessFailedCount++);
        public Task ResetAccessFailedCountAsync(User user) => Task.FromResult(user.AccessFailedCount = 0);
        public Task<int> GetAccessFailedCountAsync(User user) => Task.FromResult(user.AccessFailedCount);
        public Task<bool> GetLockoutEnabledAsync(User user) => Task.FromResult(user.LockoutEnabled);
        public Task SetLockoutEnabledAsync(User user, bool enabled) => Task.FromResult(user.LockoutEnabled = enabled);


        public Task SetPasswordHashAsync(User user, string passwordHash) => Task.FromResult(user.PasswordHash = passwordHash);
        public Task<string> GetPasswordHashAsync(User user) => Task.FromResult(user.PasswordHash);
        public Task<bool> HasPasswordAsync(User user) => Task.FromResult(true);

        public Task SetTwoFactorEnabledAsync(User user, bool enabled) => throw new NotImplementedException();
        public Task<bool> GetTwoFactorEnabledAsync(User user) => Task.FromResult(false);
    }
}
