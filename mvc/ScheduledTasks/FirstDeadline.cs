using System.Linq;
using Core.Interfaces;
using Core;
using Core.Helpers;
using Core.Entities;

namespace ScheduledTAsks
{
    public class FirstDeadline
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IWebApplicationConfig _webConfig;

        public FirstDeadline(IServiceFactory serviceFactory, IWebApplicationConfig webConfig)// email svc
        {
            _serviceFactory = serviceFactory;
            _webConfig = webConfig;
        }

        public void ManUpGroups(int semestr)
        {
            for (int i = 1; i <= Constants.MaxCourse; i++)
            {
                manUpGroupsFirstly(i, i * 2 + 1);
                manUpGroupsFirstly(i, i * 2 + 2);
            }

            for (int i = 1; i <= Constants.MaxCourse; i++)
            {
                ManUpGroupsSecondly(i, i * 2 + 1);
                ManUpGroupsSecondly(i, i * 2 + 2);
            }
        }

        /// <summary>
        /// Fills groups with stdAmount less than GroupAbstractLimit with std that registered in one group 
        /// </summary>
        private void ManUpGroupsSecondly(int course, int semestr)
        {
            var stdFilter = FilterHelper.StudentOptionList;
            stdFilter.First().Course = course;
            var students = _serviceFactory.UserService.Find(new SearchFilter<Core.Entities.User>
            {
                OptionList = stdFilter
            });

            var disciplines = _serviceFactory.DisciplineService.Find(new SearchFilter<Discipline>
            {
                OptionList = new[] { new Discipline { Semester = semestr } }
            });

            var studIdsInDiscipline = disciplines.Select(d => new
            {
                DisciplineId = d.Id,
                Students = d.StudentIds,
                Count = d.StudentIds.Count
            }).ToList();

            //find std id that registered only once
            var onceRegisteredStd = students.Where(s => s.RegisteredDisciplines?.Count == 1).ToList();

            foreach (var t in studIdsInDiscipline)
            {
                if (t.Count < _webConfig.GroupAbstractLimit)
                {
                    // students that's need to add to fill group
                    var stdAmountToAdd = _webConfig.GroupAbstractLimit - t.Count;

                    if (onceRegisteredStd.Count >= stdAmountToAdd)
                    {
                        //var stdToAdd = notRegisteredStd.GetRange(0, stdAmountToAdd);
                        int indexStd = 0;
                        int stdAdded = 0;
                        //fill this group with not registered student
                        while (stdAdded < stdAmountToAdd && indexStd < onceRegisteredStd.Count)
                        {
                            if (!studIdsInDiscipline.SingleOrDefault(el => el.DisciplineId == t.DisciplineId)
                                                   .Students.Contains(onceRegisteredStd[indexStd].Id))
                            {
                                onceRegisteredStd.Remove(onceRegisteredStd[indexStd]);
                                stdAdded++;
                            }
                            else
                            {
                                indexStd++;
                            }
                        }
                    }
                    else
                    {
                        t.Students.ForEach(s => _serviceFactory.DisciplineService.UnregisterStudent(s, t.DisciplineId));
                    }
                }
            }
        }

        /// <summary>
        /// Fills groups whith stdAmount less than GroupAbstractLimit with std that do not register
        /// </summary>
        private void manUpGroupsFirstly(int course, int semestr)
        {
            var stdFilter = FilterHelper.StudentOptionList;
            stdFilter.First().Course = course;
            var students = _serviceFactory.UserService.Find(new SearchFilter<User>
            {
                OptionList = stdFilter
            });

            var disciplines = _serviceFactory.DisciplineService.Find(new SearchFilter<Discipline>
            {
                OptionList = new[] { new Discipline { Semester = semestr } }
            });

            var studIdsInDiscipline = disciplines.Select(d => new
            {
                DisciplineId = d.Id,
                Students = d.StudentIds,
                Count = d.StudentIds.Count
            }).ToList();

            //find std id that is notregistered
            var notRegisteredStd = students.Where(s => s.RegisteredDisciplines == null ||
                                                       s.RegisteredDisciplines.Count == 0).ToList();

            foreach (var t in studIdsInDiscipline)
            {
                if (t.Count < _webConfig.GroupAbstractLimit)
                {
                    // students that's need to add to fill group
                    var stdAmountToAdd = _webConfig.GroupAbstractLimit - t.Count;

                    if (notRegisteredStd.Count >= stdAmountToAdd)
                    {
                        int stdAdded = 0;
                        //fill this group with not registered student
                        while (stdAdded < stdAmountToAdd)
                        {
                            _serviceFactory.DisciplineService.RegisterStudent(notRegisteredStd.First().Id, t.DisciplineId);
                            notRegisteredStd.Remove(notRegisteredStd.First());
                            stdAdded++;
                        }
                    }
                }
            }
        }
    }
}

