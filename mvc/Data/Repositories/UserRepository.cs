using Core.Entities;
using System.Collections.Generic;

namespace Data.Repositories
{
    class UserRepository : Repository<User, string>, IUserRepository
    {
        private static User _user;
        public UserRepository()
        {
            _user = _user ?? new User() { UserName = "fipsher123@gmail.com", PasswordHash = "Ryba5656", Email = "fipsher123@gmail.com" , AccessFailedCount = 0};
        }

        public override User Find(string id) => _user;

    }
}
