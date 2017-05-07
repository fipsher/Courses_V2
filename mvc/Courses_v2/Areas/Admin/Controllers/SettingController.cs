using Core.Entities;
using Courses_v2.Controllers;
using System.Web.Mvc;
using Core.Interfaces.Services;
using Core.Helpers;
using Core.Interfaces;
using Core;
using System.Linq;

namespace Courses_v2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public ActionResult Edit(string id, Setting setting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var settings = Service.Find(SearchFilter<Setting>.Empty);
                    var start = settings.SingleOrDefault(s => s.Name == Constants.StartDate);
                    var first = settings.SingleOrDefault(s => s.Name == Constants.FirstDeadline);
                    var second = settings.SingleOrDefault(s => s.Name == Constants.SecondDeadline);
                    switch (setting.Name)
                    {
                        case Constants.StartDate:
                            if (setting.Value < first.Value)
                            {
                                Service.Update(id, setting);
                            }
                            else
                            {
                                ViewBag.ValidationError = "Start date should be greater than first deadline date";
                            }
                            break;
                        case Constants.FirstDeadline:
                            if (setting.Value < second.Value && setting.Value > start.Value)
                            {
                                Service.Update(id, setting);
                            }
                            else
                            {
                                ViewBag.ValidationError = "First deadline date should be greater than start date and less than second deadline";
                            }
                            break;
                        case Constants.SecondDeadline:
                            if (setting.Value > first.Value)
                            {
                                Service.Update(id, setting);
                            }
                            else
                            {
                                ViewBag.ValidationError = "Second deadline date should be greater than first deadline date";
                            }
                            break;
                    }
                    // insert setting validation
                    // user Strings.PropName to validate it
                    Service.Update(id, setting);
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
