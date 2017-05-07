using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces;
using System.Collections.Generic;

namespace Data.Services
{
    class SettingService : BaseService<Setting>, ISettingService
    {
        public SettingService(IRepositoryBootstrapper repositoryBootstrapper) : base(repositoryBootstrapper)
        {
        }

        //public void UpdateMany(IEnumerable<Setting> settings)
        //{
        //    foreach (var setting in settings)
        //    {
        //        Repository.Update(setting);
        //    }
        //}
    }
}
