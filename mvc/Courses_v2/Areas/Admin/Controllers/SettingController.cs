using Core.Entities;
using Courses_v2.Controllers;
using System.Web.Mvc;
using Core.Interfaces.Services;
using Core.Helpers;
using Core.Interfaces;
using Core;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Core.Extensions;
using System.Threading.Tasks;

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

        // POST: Admin/Setting/Edit/5
        [HttpPost]
        public async Task<JsonResult> Edit(string id, Setting setting)
        {
            string error = string.Empty;
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
                            if (first == null || setting.Value < first.Value)
                            {
                                if (start == null)
                                {
                                    Service.Add(setting);
                                }
                                else
                                {
                                    Service.Update(id, setting);
                                }

                                using (var client = new HttpClient())
                                {
                                    await client.PostObjectAsync("http://localhost:2000/api/scheduler/schedule-wave",
                                        new
                                        {
                                            DayOfMonth = setting.Value.Value.Day,
                                            Month = setting.Value.Value.Month,
                                            Year = setting.Value.Value.Year,
                                            Wave = 0
                                        });
                                }
                            }
                            else
                            {
                                error = "Start date should be greater than first deadline date";
                            }
                            break;
                        case Constants.FirstDeadline:
                            if ((second == null || setting.Value < second.Value)
                                &&
                                (start == null || setting.Value > start.Value))
                            {
                                if (first == null)
                                {
                                    Service.Add(setting);
                                }
                                else
                                {
                                    Service.Update(id, setting);
                                }
                            }
                            else
                            {
                                error = "First deadline date should be greater than start date and less than second deadline";
                            }
                            break;
                        case Constants.SecondDeadline:
                            if (first == null || setting.Value > first.Value)
                            {
                                if (second == null)
                                {
                                    Service.Add(setting);
                                }
                                else
                                {
                                    Service.Update(id, setting);
                                }
                            }
                            else
                            {
                                error = "Second deadline date should be greater than first deadline date";
                            }
                            break;
                    }
                    // insert setting validation
                    // user Strings.PropName to validate it
                    return Json(new
                    {
                        isSuccess = string.IsNullOrEmpty(error),
                        message = error
                    });
                }
                else
                {
                    error = "Поле не можу бути пустим";
                }
            }
            catch
            {
                throw;
            }
            return Json(new
            {
                isSuccess = string.IsNullOrEmpty(error),
                message = error
            });
        }
    }
}