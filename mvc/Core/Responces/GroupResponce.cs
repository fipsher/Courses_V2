using Core.Entities;
using System.ComponentModel;

namespace Core.Responces
{
    public class GroupResponce
    {
        public GroupResponce(Group group)
        {
            Id = group.Id;
            Course = group.Course;
            Name = group.Name;
        }
        public string Id { get; set; }
        [DisplayName("Назва")]
        public string Name { get; set; }
        [DisplayName("Кафедра")]
        public Cathedra Cathedra { get; set; }
        [DisplayName("Курс")]
        public int? Course { get; set; }
    }
}
