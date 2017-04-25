using Core.Interfaces;
using System.Web.Mvc;

namespace Courses_v2.Controllers
{
    public class BaseController<TEntity, TService> : Controller where TEntity : class, IEntity where TService : class, IService<TEntity>
    {
        protected TService Service;
        public BaseController(TService service)
        {
            Service = service;
        }
    }
}