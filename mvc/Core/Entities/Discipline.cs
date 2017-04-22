using System;
using System.Collections.Generic;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class Discipline : Entity
    {
        public string Name { get; set; }
        public Guid ProviderCathedraId { get; set; }
        public List<Guid> SubscriberCatherdaIds { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public int Semestr { get; set; }

        public List<string> Students { get; set; }
    }
}
