using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class User : Entity, IUser
    {
        public string Login { get; }

        public Guid GroupId { get; set; }
        public string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual List<Role> Roles { get; }
    }
}
