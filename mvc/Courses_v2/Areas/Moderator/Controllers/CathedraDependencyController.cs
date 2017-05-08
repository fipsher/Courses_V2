using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses_v2.Areas.Moderator.Controllers
{
    public class CathedraDependencyController : Controller
    {
        private readonly IServiceFactory _serviceFactory;

        public CathedraDependencyController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public ActionResult Setup(string cathedraId, int semester)
        {
            var cathedras = _serviceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty);
            ViewBag.Cathedras = cathedras;

            var cathedra = cathedras.SingleOrDefault(c => c.Id == cathedraId);



            return View(cathedra);
        }
        [HttpPost]
        public ActionResult Setup(string id, Cathedra cathedra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _serviceFactory.CathedraService.Update(id, cathedra);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                throw;
            }
            return View();
        }
    }
}