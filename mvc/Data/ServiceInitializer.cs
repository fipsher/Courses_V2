using Core.Interfaces;
using Data.Repositories;
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
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IDataService>().To<IDataService>();
        }
    }
}