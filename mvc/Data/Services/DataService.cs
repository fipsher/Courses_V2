using Core.Entities;
using Core.Interfaces;
using Data.Repositories;

namespace Data.Services
{
    public class DataService : IDataService
    {
        private IUserRepository _users;
        private IDisciplineRepository _disciplines;
        private IWebApplicationConfig _webConfig;

        public DataService(IWebApplicationConfig webConfig)
        {
            _webConfig = webConfig;
        }

        public IUserRepository Users => _users ?? (_users = new UserRepository(_webConfig.WebApiUrl));
        public IDisciplineRepository Disciplines => _disciplines ?? (_disciplines = new DisciplineRepository(_webConfig.WebApiUrl));
    }
}