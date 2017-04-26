using System;

namespace Core.Interfaces
{
    public interface IRepositoryBootstrapper
    {
        IRepository this[Type type] { get; }
    }
}
