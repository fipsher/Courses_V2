using Core.Entities;
using Core.Interfaces;
using Data.Repositories;

namespace Data.Services
{
    public class DataService : IDataService
    {
        private Repository<User, string> _users;
        private IWebApplicationConfig _webConfig;

        public DataService(IWebApplicationConfig webConfig)
        {
            _webConfig = webConfig;
        }

        public Repository<User, string> Users => _users ?? (_users = new UserRepository(_webConfig.WebApiUrl));
    }
}