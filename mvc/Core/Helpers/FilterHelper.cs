using Core.Entities;
using System.Collections.Generic;
using static Core.Enums.Enums;

namespace Core.Helpers
{
    public class FilterHelper
    {
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

        public static List<T> OptionListByEntity<T>(T entity) where T: Entity => new List<T>
        {
            entity
        };
        #endregion
    }
}
