using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses_v2.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ParentName { get; set; }
        public string GroupName { get; set; }
        public string Course { get; set; }
        public string Faculty { get; set; }
    }
}