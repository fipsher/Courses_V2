using Core.Entities;
using Core.Interfaces;
using Data.Repositories;
using Microsoft.AspNet.Identity.Owin;

namespace Data.Services
{
    public class DataService : IDataService
    {
        private Repository<User, string> _users;

        //public DataService(IWebService service)
        //{
        //    _service = service
        //}

        public Repository<User, string> Users => _users ?? (_users = new UserRepository());//TODO: inject custom WebClient service
    }
}