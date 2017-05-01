using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #endregion
    }
}
