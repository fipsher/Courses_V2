﻿using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Responces;
using Courses_v2.Areas.Admin.Models;
using Courses_v2.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Core.Enums.Enums;

namespace Courses_v2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class GroupController : BaseController<Group, IGroupService>
    {
        private readonly IUserService _userService;

        public GroupController(IServiceFactory serviceFactory) : base(serviceFactory, serviceFactory.GroupService)
        {
            _userService = serviceFactory.UserService;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            var users = Service.FindGroupResponce(SearchFilter<Group>.Default);
            return View(users.ToList());
        }
        // Post: Admin/User
        [HttpPost]
        public ActionResult Index(Group filter = null)
        {
            filter = filter ?? new Group();
            var searchFilter = SearchFilter<Group>.Default;

            searchFilter.OptionList = FilterHelper.OptionListByEntity<Group>(filter);

            var users = Service.FindGroupResponce(searchFilter);
            return View(users.ToList());
        }

        // GET: Admin/group/Details/5
        public ActionResult Details(string id)
        {
            var groups = Service.FindGroupResponce(SearchFilter<Group>.FilterById(id));

            return View(groups.SingleOrDefault());
        }

        // GET: Admin/group/Create
        public ActionResult Create()
        {
            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty);
            var group = new Group
            {
                DisciplineConfiguration = new List<DisciplineConfiguration>
                {
                    new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester = 1},
                    new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester =  2},
                    new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 1},
                    new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 2}
                }
            };
            return View(group);
        }
        // POST: Admin/group/Create
        [HttpPost]
        public ActionResult Create(Group group)
        {
            try
            {
                if (!group.DisciplineConfiguration.Any())
                {
                    group.DisciplineConfiguration = new List<DisciplineConfiguration>
                        {
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester = 1},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester =  2},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 1},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 2}
                        };
                }
                if (ModelState.IsValid)
                {
                    Service.Add(group);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                throw;
            }
            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty);
            return View(group);
        }

        // GET: Admin/group/Edit/5
        public ActionResult Edit(string id)
        {
            var group = Service.Find((new SearchFilter<Group>() { OptionList = new[] { new Group() { Id = id } } })).SingleOrDefault();
            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty);

            return View(group);
        }

        // POST: Admin/group/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Group group)
        {
            try
            {
                if (!group.DisciplineConfiguration.Any())
                {
                    group.DisciplineConfiguration = new List<DisciplineConfiguration>
                        {
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester = 1},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Socio, RequiredAmount = 1, Semester =  2},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 1},
                            new DisciplineConfiguration{DisciplineType = DisciplineType.Special, RequiredAmount = 1, Semester = 2}
                        };
                }
                if (ModelState.IsValid)
                {
                    Service.Update(id, group);
                    var users = _userService.Find(SearchFilter<User>.FilterByEntity(new User { GroupId = group.Id } ));
                    foreach (var user in users)
                    {
                        user.Course = group.Course;
                        _userService.Update(user.Id, user);
                    }
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                throw;
            }
            ViewBag.Cathedras = ServiceFactory.CathedraService.Find(SearchFilter<Cathedra>.Empty);
            return View(group);
        }


        public ActionResult EditDisciplines(string id)
        {
            IEnumerable<GroupDisciplineModel> disciplines = Service.GetGroupDisciplines(id);
            ViewBag.Id = id;
            return View(disciplines);
        }


        public ActionResult AddDiscipline(string id)
        {
            var disciplines = Service.GetNotSubscribedDisciplines(id)
                                     .Select(el => new { Id = el.Id, Name = $"{el.Name} (Семестр {el.Semester})" });

            var model = new AssignDisciplineModel
            {
                Id = id,
                DisciplineList = new SelectList(disciplines, "Id", "Name", null)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AssignDiscipline(string id, string disciplineId)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(disciplineId))
            {
                Service.AddDiscipline(id, disciplineId);
                return RedirectToAction("EditDisciplines", new { id = id });
            }

            return RedirectToAction("EditDisciplines", new { id = id });
        }


        [HttpPost]
        public ActionResult DeleteFromGroup(string groupId, string disciplineId)
        {
            if (ModelState.IsValid)
            {
                Service.RemoveDisciplineFromGroup(groupId, disciplineId);
                return RedirectToAction("EditDisciplines", new { id = groupId });
            }

            return RedirectToAction("EditDisciplines", new { id = groupId });
        }
        // GET: Admin/group/Delete/5
        public ActionResult Delete(string id)
        {
            var groups = Service.Find((new SearchFilter<Group>()
            {
                OptionList = new[] { new Group() { Id = id } }
            }));

            return View(groups?.SingleOrDefault());
        }

        // POST: Admin/group/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Group group)
        {
            try
            {
                Service.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }
    }
}
