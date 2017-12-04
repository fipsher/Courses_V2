using Core.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static Core.Enums.Enums;

namespace Courses_v2.Models
{
    public class ApplicationUser : User, IUser
    {
        public ApplicationUser(User user)
        {
            this.Course = user.Course;
            this.Email = user.Email;
            this.GroupId = user.GroupId;
            this.Id = user.Id;
            this.Login = user.Login;
            this.Password = user.Password;
            this.PhoneNumber = user.PhoneNumber;
            this.Disciplines = user.Disciplines;
            this.Roles = user.Roles;
            this.UserName = user.UserName;
        }

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