using Core.Entities;
using System.Collections.Generic;
using static Core.Enums.Enums;

namespace Core.Helpers
{
    public class FilterHelper
    {
        public static List<T> OptionListByEntity<T>(T entity) where T : Entity => new List<T>
        {
            entity
        };

        #region Users
        public static List<User> StudentOptionList => new List<User>
        {
            new User { Roles = new List<Role> { Role.Student } }
        };
        public static List<User> LecturerOptionList => new List<User>
        {
            new User { Roles = new List<Role> { Role.Lecturer } }
        };
        public static List<User> ModeratorOptionList => new List<User>
        {
            new User { Roles = new List<Role> { Role.Moderator } }
        };
        #endregion


        #region Discipline
        public static List<Discipline> SocialDisciplines(int? course) => new List<Discipline>
        {
            new Discipline { DisciplineType = DisciplineType.Socio, Semester = course * 2 + 1},
            new Discipline { DisciplineType = DisciplineType.Socio, Semester = course * 2 + 2 }
        };
        public static List<Discipline> SpecialDisciplines(int? course, string name = null) => new List<Discipline>
        {
            new Discipline { DisciplineType = DisciplineType.Special, Semester = course * 2 + 1, Name = name },
            new Discipline { DisciplineType = DisciplineType.Special, Semester = course * 2 + 2, Name = name }
        };
        #endregion
    }
}
