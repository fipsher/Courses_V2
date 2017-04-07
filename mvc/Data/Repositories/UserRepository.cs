using Core.Entities;
using Core.Interfaces;

namespace Data.Repositories
{
    class UserRepository : Repository<User, string>, IUserRepository
    {
        private static User _user;
        public UserRepository(string webApiUrl) : base(webApiUrl)
        {
            _user = _user ?? new User() { UserName = "fipsher123@gmail.com", Password = "Ryba5656", Email = "fipsher123@gmail.com" , AccessFailedCount = 0};
        }

        public override User Find(string id) => _user;

    }
}
