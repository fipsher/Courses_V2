using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class User : Entity
    {
        [DisplayName("Група")]
        public string GroupId { get; set; }

        [DisplayName("Ім'я")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string UserName { get; set; }

        [DisplayName("Пошта")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Password { get; set; }

        [DisplayName("Номер телефону")]
        public string PhoneNumber { get; set; }

        [DisplayName("Ролі")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public List<Role> Roles { get; set; }

        [DisplayName("Дисципліни")]
        public List<string> DisciplineIds { get; set; }

        //[DisplayName("Курс")]
        //[Range(0, Constants.MaxCourse, ErrorMessage = "Min = 0, Max = 6")]
        public int? Course { get; set; }

        [DisplayName("Логін")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Login { get; set; }
    }
}
