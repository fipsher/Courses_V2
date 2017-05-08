using Core.Entities;

namespace Core.Responces
{
    class StudentGroupResponce
    {
        public StudentGroupResponce(Group group)
        {
            Id = group.Id;
            Semester = group.Course;
        }
        public string Id { get; set; }
        public Cathedra Cathedra { get; set; }
        public int Semester { get; set; }
    }
}
