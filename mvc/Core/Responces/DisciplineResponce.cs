using Core.Entities;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public Cathedra ProviderCathedra { get; set; }
        public List<Cathedra> SubscriberCathedras { get; set; }
        public DisciplineType? DisciplineType { get; set; }
        public int? Semester { get; set; }
        public string Description { get; set; }
        public User Lecturer { get; set; }

        public List<User> Students { get; set; }
    }
}
