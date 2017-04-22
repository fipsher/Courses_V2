using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Data.Services
{
    public class RepositoryBootstrapper
    {
        private readonly Dictionary<Type, IRepository<IEntity>> RepositoryResolver = new Dictionary<Type, IRepository<IEntity>>();

        private void Bootstrap()
        {
            // fill up Dictionary
        }

        public RepositoryBootstrapper()
        {
            Bootstrap();
        }

        public IRepository<IEntity> this[Type type]
        {
            get => RepositoryResolver[type];
        }
    }
}