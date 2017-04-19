using Core.Interfaces;
using System;

namespace Core.Entities
{
    public class StudentGroup : IEntity<Guid>
    {
        public Guid Id { get; }
        public Guid CathedraId { get; set; }
        public int Semestr { get; set; }
    }
}
