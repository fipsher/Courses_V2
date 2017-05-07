using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class Discipline : Entity
    {
        [DisplayName("Назва")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Name { get; set; }

        [DisplayName("Кафедра-провайдер")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string ProviderCathedraId { get; set; }

        [DisplayName("Кафедри підписники")]
        public List<string> SubscriberCathedraIds { get; set; }

        [DisplayName("Тип дисципліни")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public DisciplineType? DisciplineType { get; set; }

        [DisplayName("Семестр")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public int? Semester { get; set; }

        [DisplayName("Опис")]
        public string Description { get; set; }

        [DisplayName("Лектор")]
        public string LecturerId { get; set; }

        public List<string> StudentIds { get; set; }
    }
}
