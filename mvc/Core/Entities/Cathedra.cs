
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Cathedra : Entity
    {
        [DisplayName("Назва")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Name{ get; set; }

        public List<DisciplineSubscriptions> DisciplineSubscriptions { get; set; }
    }

    public class DisciplineSubscriptions
    {
        public string DisciplineId { get; set; }
        public int Semestr { get; set; }
    }
}
