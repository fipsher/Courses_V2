using Core.Entities;
using Core.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class RepositoryBootstrapper
    {
        private readonly Dictionary<Type, IRepository> RepositoryResolver = new Dictionary<Type, IRepository>();

        private void Bootstrap(string webApiUrl)
        {
            RepositoryResolver.Add(typeof(User), new Repository<User>(webApiUrl));
            RepositoryResolver.Add(typeof(Discipline), new Repository<Discipline>(webApiUrl));
        }

        public RepositoryBootstrapper(IWebApplicationConfig webConfig)
        {
            Bootstrap(webConfig.WebApiUrl);
        }

        public IRepository this[Type type]
        {
            get => RepositoryResolver[type];
        }
    }
}