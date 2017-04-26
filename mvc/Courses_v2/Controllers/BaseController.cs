using Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Courses_v2.Controllers
{
    public class BaseController<TEntity, TService> : Controller where TEntity : class, IEntity where TService : class, IService<TEntity>
    {
        protected TService Service;

        public string _userId;
        public string UserId
        {
            get
            {
                return _userId ?? (_userId ?? User.Identity.GetUserId());
            }
        }

        public BaseController(TService service)
        {
            Service = service;
        }
    }
}