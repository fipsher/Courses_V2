using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {
        User GetByEmail(string email);
    }
}