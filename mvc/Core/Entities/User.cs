using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class User : Entity
    {
        [DisplayName("Логін")]
        [Required]
        public string Login { get; }

        [DisplayName("Група")]
        public string GroupId { get; set; }
        [DisplayName("Ім'я")]
        [Required]
        public string UserName { get; set; }
        [DisplayName("Пошта")]
        public string Email { get; set; }
        [DisplayName("Пароль")]
        [Required]
        public string Password { get; set; }
        [DisplayName("Номер телефону")]
        public string PhoneNumber { get; set; }
        [DisplayName("Ролі")]
        [Required]
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
