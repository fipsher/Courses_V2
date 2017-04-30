
using Newtonsoft.Json;

namespace Core.Entities
{
    public class StudentGroup : Entity
    {
        public string Name { get; set; }
        public string CathedraId { get; set; }
        public int Semester { get; set; }
    }
}
