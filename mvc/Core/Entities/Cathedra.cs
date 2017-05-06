
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Cathedra : Entity
    {
        [DisplayName("Назва")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Name{ get; set; }
    }
}
