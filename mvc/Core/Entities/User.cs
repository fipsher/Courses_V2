﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class User : Entity, IUser
    {
        [DisplayName("Логін")]
        public string Login { get; }

        [DisplayName("Група")]
        public string GroupId { get; set; }
        [DisplayName("Ім'я")]
        public string UserName { get; set; }
        [DisplayName("Пошта")]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [DisplayName("Номер лелефону")]
        public string PhoneNumber { get; set; }
        [DisplayName("Ролі")]
        public List<Role> Roles { get; set; }

        [DisplayName("Дисципліни")]
        public List<string> DisciplineIds { get; set; }
    }
}
