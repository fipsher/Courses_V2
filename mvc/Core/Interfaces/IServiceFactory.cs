using Core.Interfaces.Services;
namespace Core.Interfaces
{
    public interface IServiceFactory
    {
        IUserService UserService { get; }
        ICathedraService CathedraService { get; }
        IDisciplineService DisciplineService { get; }
        ISettingService SettingService { get; }
        IGroupService GroupService{get;}
    }
}
