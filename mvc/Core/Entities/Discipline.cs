using System.Collections.Generic;
using System.ComponentModel;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class Discipline : Entity
    {
        [DisplayName("Назва")]
        public string Name { get; set; }
        [DisplayName("Кафедра-провайдер")]
        public string ProviderCathedraId { get; set; }
        [DisplayName("Кафедри підписники")]
        public List<string> SubscriberCathedraIds { get; set; }
        [DisplayName("Тип дисципліни")]
        public DisciplineType DisciplineType { get; set; }
        [DisplayName("Семестр")]
        public int Semester { get; set; }
        [DisplayName("Опис")]
        public string Description { get; set; }
        [DisplayName("Лектор")]
        public string LecturerId { get; set; }

        public List<string> StudentIds { get; set; }
    }
}
