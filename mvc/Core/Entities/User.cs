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
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public List<Role> Roles { get; set; }

        public List<string> ChoosenDisciplineIds { get; set; }
    }
}
