
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Group : Entity
    {
        [DisplayName("Назва")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string Name { get; set; }

        [DisplayName("Кафедра")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public string CathedraId { get; set; }

        [DisplayName("Семестр")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        public int Course { get; set; }

        //[DisplayName("Кількість не ...")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Поле обов'язкове до заповнення")]
        //public int AmountOfNonSocialDisciplines { get; set; }
    }
}
