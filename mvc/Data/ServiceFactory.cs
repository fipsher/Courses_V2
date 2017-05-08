using Core.Interfaces;
using Core.Interfaces.Services;
using Data.Services;

namespace Data
{
    internal class ServiceFactory : IServiceFactory
    {
        private readonly IRepositoryBootstrapper _bootstrapper;

        private IUserService _userService { get; set; }
        private ICathedraService _cathedraService { get; set; }
        private IDisciplineService _disciplineService { get; set; }
        private ISettingService _settingService { get; set; }
        private IGroupService _groupService { get; set; }

        public ServiceFactory(IRepositoryBootstrapper bootstrapper)
        {
            _bootstrapper = bootstrapper;
        }

        public IUserService UserService { get => _userService ?? (_userService = new UserService(_bootstrapper)); }
        public ICathedraService CathedraService { get => _cathedraService ?? (_cathedraService = new CathedraService(_bootstrapper)); }
        public IDisciplineService DisciplineService { get => _disciplineService ?? (_disciplineService = new DisciplineService(_bootstrapper)); }
        public ISettingService SettingService { get => _settingService ?? (_settingService = new SettingService(_bootstrapper)); }
        public IGroupService GroupService { get => _groupService ?? (_groupService = new GroupService(_bootstrapper)); }
    }
}
