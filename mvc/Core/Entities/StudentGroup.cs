
using Newtonsoft.Json;
using System.ComponentModel;

namespace Core.Entities
{
    public class StudentGroup : Entity
    {
        [DisplayName("Назва")]
        public string Name { get; set; }
        [DisplayName("Кафедра")]
        public string CathedraId { get; set; }
        [DisplayName("Семестр")]
        public int Semester { get; set; }
    }
}
