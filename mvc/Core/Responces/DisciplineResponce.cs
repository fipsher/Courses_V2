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
            Semestr = discipline.Semestr;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Cathedra ProviderCathedra { get; set; }
        public List<Cathedra> SubscriberCathedras { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public int Semestr { get; set; }

        public List<User> Students { get; set; }
    }
}
