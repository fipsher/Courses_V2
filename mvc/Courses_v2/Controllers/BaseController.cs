using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Courses_v2.Controllers
{
    public class BaseController<TEntity, TService> : Controller where TEntity : Entity, new() where TService : class, IService<TEntity>
    {
        protected TService Service;
        protected IServiceFactory ServiceFactory;

        private string _userId;
        public string UserId
        {
            get
            {
                return _userId ?? (_userId ?? User.Identity.GetUserId());
            }
        }

        public BaseController(IServiceFactory serviceFactory, TService service)
        {
            Service = service;
            ServiceFactory = serviceFactory;
        }
    }
}