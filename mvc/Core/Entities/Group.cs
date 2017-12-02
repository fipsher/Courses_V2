
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class Group : Entity
    {
        [DisplayName("Назва")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Name { get; set; }

        [DisplayName("Кафедра")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string CathedraId { get; set; }

        [DisplayName("Курс")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public int? Course { get; set; }

        //[DisplayName("Кількість не ...")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        //public int AmountOfNonSocialDisciplines { get; set; }

        public List<string> DisciplineSubscriptions { get; set; }

        public DisciplineConfiguration DisciplineConfiguration { get; set; }

    }

    public class DisciplineConfiguration
    {
        public int RequiredAmount { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public int Semester { get; set; }
    }
}
