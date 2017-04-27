using Core.Entities;

namespace Core.Responces
{
    class StudentGroupResponce
    {
        public StudentGroupResponce(StudentGroup group)
        {
            Id = group.Id;
            Semester = group.Semester;
        }
        public string Id { get; set; }
        public Cathedra Cathedra { get; set; }
        public int Semester { get; set; }
    }
}
