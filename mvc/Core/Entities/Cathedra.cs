
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

        // TODO: ask Andrew Roman 
        public List<CathedraSubscribers> CathedraSubscribers { get; set; }
    }

    public class CathedraSubscribers
    {
        public string CathedraId { get; set; }
        public int Semestr { get; set; }
    }
}
