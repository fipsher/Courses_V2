using Core.Entities;

namespace Core.Responces
{
    class StudentGroupResponce
    {
        public StudentGroupResponce(StudentGroup group)
        {
            Id = group.Id;
            Semestr = group.Semestr;
        }
        public string Id { get; set; }
        public Cathedra Cathedra { get; set; }
        public int Semestr { get; set; }
    }
}
