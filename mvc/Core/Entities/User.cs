using Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : IUser, IEntity<string>
    {
        public string Id { get; }

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
