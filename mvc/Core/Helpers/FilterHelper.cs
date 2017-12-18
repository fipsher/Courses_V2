using Core.Entities;
using System.Collections.Generic;
using System.Linq;
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
        public static List<Discipline> SocialDisciplines(int course, bool? isAvailable = null) => new List<Discipline>
        {
            new Discipline { DisciplineType = DisciplineType.Socio, Semester = course * 2 + 1, IsAvailable = isAvailable },
            new Discipline { DisciplineType = DisciplineType.Socio, Semester = course * 2 + 2, IsAvailable = isAvailable }
        };
        public static List<Discipline> SpecialDisciplines(int course, bool? isAvailable = null) => new List<Discipline>
        {
            new Discipline { DisciplineType = DisciplineType.Special, Semester = course * 2 + 1, IsAvailable = isAvailable },
            new Discipline { DisciplineType = DisciplineType.Special, Semester = course * 2 + 2, IsAvailable = isAvailable }
        };
        public static List<Discipline> SpecialDisciplines(int course, List<string> disciplineIds)
        {
            var filter = disciplineIds.Select(el => new Discipline
            {
                DisciplineType = DisciplineType.Special,
                Semester = course * 2 + 1,
            });

            filter = filter.Concat(disciplineIds.Select(el => new Discipline
            {
                DisciplineType = DisciplineType.Special,
                Semester = course * 2 + 2,
            }));

            return filter.ToList();
        }
        #endregion
    }
}
