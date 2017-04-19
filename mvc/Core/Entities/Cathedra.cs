using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cathedra : IEntity<Guid>
    {
        public Guid Id { get; }
        public string Name{ get; set; }
    }
}
