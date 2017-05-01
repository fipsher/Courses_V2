using Core.Interfaces;
using Core.Interfaces.Services;
using Data.Services;
using Ninject;

namespace Data
{
    public static class ServiceInitializer
    {
        /// <summary>
        /// Register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IDisciplineService>().To<DisciplineService>();
            kernel.Bind<ICathedraService>().To<CathedraService>();
            kernel.Bind<IStudentGroupService>().To<StudentGroupService>();
            kernel.Bind<ISettingService>().To<SettingService>();

            kernel.Bind<IRepositoryBootstrapper>().To<RepositoryBootstrapper>();
            kernel.Bind<IServiceFactory>().To<ServiceFactory>();
        }
    }
}