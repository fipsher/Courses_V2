using Core.Entities;
using Core.Enums;
using Core.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses_v2.Areas.Student.Models
{
    public class StudentDisciplines
    {
        public List<GroupDisciplineModel> Disciplines { get; set; }

        public List<Discipline> SudentChoice { get; set; }

        public List<DisciplineConfiguration> DisciplineConfiguration { get; set; }

        public Enums.DisciplineType Type;
    }
}