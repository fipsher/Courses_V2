using Core.Entities;
using Courses_v2.Controllers;
using System.Web.Mvc;
using Core.Interfaces.Services;
using Core.Helpers;
using System.Collections.Generic;
using Core.Interfaces;

namespace Courses_v2.Areas.Admin.Controllers
{
    public class SettingController : BaseController<Setting, ISettingService>
    {
        public SettingController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.SettingService)
        {
        }

        // GET: Admin/Setting
        public ActionResult Index()
        {
            var settings = Service.Find(SearchFilter<Setting>.Empty);

            return View(settings);
        }

        // GET: Admin/Setting/Edit/5
        public ActionResult Edit(string id)
        {
            var settings = Service.Find((SearchFilter<Setting>.Empty));

            return View(settings);
        }

        // POST: Admin/Setting/Edit/5
        [HttpPost]
        public ActionResult Edit(List<Setting> settings)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // insert setting validation
                    // user Strings.PropName to validate it
                    Service.UpdateMany(settings);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                //
            }
            return View();
        }
    }
}
