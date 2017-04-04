using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace Data.Services
{
    public class CustomUserStore : IUserStore<User>
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
    }
}
