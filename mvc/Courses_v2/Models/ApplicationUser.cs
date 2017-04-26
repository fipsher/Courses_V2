using Core.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static Core.Enums.Enums;

namespace Courses_v2.Models
{
    public class ApplicationUser : User
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            this.Roles.ForEach(r =>
            {
                userIdentity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(Role), r)));
            });
            return userIdentity;
        }
    }
}