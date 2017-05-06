using System;
using System.Collections.Generic;
using System.ComponentModel;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class User : Entity
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
        [DisplayName("Номер телефону")]
        public string PhoneNumber { get; set; }
        [DisplayName("Ролі")]
        public List<Role> Roles { get; set; }
        [DisplayName("Дисципліни")]
        public List<DisciplineRegister> RegisteredDisciplines { get; set; }
        [DisplayName("Курс")]
        public int Course { get; set; }
    }

    public class DisciplineRegister
    {
        public string DisciplineId { get; set; }
        public DateTime DateTime { get; set; }
        public DisciplineType DisciplineType { get; set; }
    }
}
