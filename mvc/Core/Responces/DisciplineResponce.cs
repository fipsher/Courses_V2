using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using static Core.Enums.Enums;

namespace Core.Responces
{
    public class DisciplineResponce
    {
        public DisciplineResponce(Discipline discipline)
        {
            Id = discipline.Id;
            Name = discipline.Name;
            DisciplineType = discipline.DisciplineType;
            Semester = discipline.Semester;
            Description = discipline.Description;
        }

        public string Id { get; set; }
        [DisplayName("Назва")]
        public string Name { get; set; }
        [DisplayName("Кафедра-провайдер")]
        public Cathedra ProviderCathedra { get; set; }
        //[DisplayName("Назва")]
        //public List<Cathedra> SubscriberCathedras { get; set; }
        [DisplayName("Тип дисципліни")]
        public DisciplineType? DisciplineType { get; set; }
        [DisplayName("Семестр")]
        public int? Semester { get; set; }
        [DisplayName("Опис")]
        public string Description { get; set; }
        [DisplayName("Лектор")]
        public User Lecturer { get; set; }

        public List<User> Students { get; set; }
    }
}
