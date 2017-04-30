
using System.ComponentModel;

namespace Core.Entities
{
    public class Cathedra : Entity
    {
        [DisplayName("Назва")]
        public string Name{ get; set; }
    }
}
