﻿using Core.Entities;
using Courses_v2.Controllers;
using System.Web.Mvc;
using Core.Interfaces.Services;
using Core.Helpers;
using System.Linq;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class CathedraController : BaseController<Cathedra, ICathedraService>
    {
        public CathedraController(ICathedraService service) : base(service)
        {
        }

        // GET: Admin/Cathedra
        public ActionResult Index(int skip = 0, int take = 100, string nameFilter = "")
        {
            var catherdas = Service.Find((new SearchFilter<Cathedra>
            {
                Take = take,
                Skip = skip,
                Query = new[] { new Cathedra { Name = nameFilter } }
            }));

            return View(catherdas);
        }

        // GET: Admin/Cathedra/Details/5
        public ActionResult Details(string id)
        {
            var catherdas = Service.Find((new SearchFilter<Cathedra>
            {
                Query = new[] { new Cathedra() { Id = id } }
            }));

            return View(catherdas.SingleOrDefault());
        }

        // GET: Admin/Cathedra/Create
        public ActionResult Create() => View();

        // POST: Admin/Cathedra/Create
        [HttpPost]
        public ActionResult Create(Cathedra cathedra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Add(cathedra);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }

        // GET: Admin/Cathedra/Edit/5
        public ActionResult Edit(string id)
        {
            var disciplines = Service.Find((new SearchFilter<Cathedra>() { Query = new[] { new Cathedra() { Id = id } } }));

            return View(disciplines.SingleOrDefault());
        }

        // POST: Admin/Cathedra/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Cathedra cathedra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.Update(cathedra);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }

        // GET: Admin/Cathedra/Delete/5
        public ActionResult Delete(string id)
        {
            var disciplines = Service.Find((new SearchFilter<Cathedra>()
            {
                Query = new[] { new Cathedra() { Id = id } }
            }));

            return View(disciplines?.SingleOrDefault());
        }

        // POST: Admin/Cathedra/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Cathedra cathedra)
        {
            try
            {
                Service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}