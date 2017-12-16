using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses_v2.Areas.Admin.Models
{
    public class AssignDisciplineModel
    {
        [Required]
        public string Id { get; internal set; }
        [Required(ErrorMessage = "Поле обовязкове до заповнення")]
        public string DisciplineId { get; internal set; }
        public SelectList DisciplineList { get; internal set; }
    }
}