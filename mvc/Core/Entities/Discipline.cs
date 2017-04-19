using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.Enums;

namespace Core.Entities
{
    public class Discipline : IEntity<Guid>
    {
        public Guid Id {get;}
        public string Name { get; set; }
        public Guid ProviderCathedraId { get; set; }
        public List<Guid> SubscriberCatherdaIds { get; set; }
        public DisciplineType DisciplineType { get; set; }
        public int Semestr { get; set; }

        public List<string> Students { get; set; }
    }
}
