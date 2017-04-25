using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface ISettingService : IService<Setting>
    {
        void UpdateMany(IEnumerable<Setting> settings);
    }
}
