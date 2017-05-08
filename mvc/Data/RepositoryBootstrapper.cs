using Core.Entities;
using Core.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Data
{
    internal class RepositoryBootstrapper : IRepositoryBootstrapper
    {
        private readonly Dictionary<Type, IRepository> RepositoryResolver = new Dictionary<Type, IRepository>();

        private void Bootstrap(string webApiUrl)
        {
            RepositoryResolver.Add(typeof(User), new Repository<User>(webApiUrl));
            RepositoryResolver.Add(typeof(Discipline), new Repository<Discipline>(webApiUrl));
            RepositoryResolver.Add(typeof(Cathedra), new Repository<Cathedra>(webApiUrl));
            RepositoryResolver.Add(typeof(Setting), new Repository<Setting>(webApiUrl));
            RepositoryResolver.Add(typeof(Group), new Repository<Group>(webApiUrl));
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