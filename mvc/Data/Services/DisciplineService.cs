using System.Collections.Generic;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using System.Linq;
using Core.Responces;
using static Core.Enums.Enums;
using Core;
using System;

namespace Data.Services
{
    internal class DisciplineService : BaseService<Discipline>, IDisciplineService
    {
        private readonly IRepository<Cathedra> _cathedraRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Setting> _settingRepo;

        public DisciplineService(IRepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
            _cathedraRepo = (IRepository<Cathedra>)repositoryStrategy[typeof(Cathedra)];
            _userRepo = (IRepository<User>)repositoryStrategy[typeof(User)];
            _settingRepo = (IRepository<Setting>)repositoryStrategy[typeof(Setting)];
        }

        public List<DisciplineResponce> FindDisciplineResponse(SearchFilter<Discipline> filter, bool includingSubscriberCathedras = false, bool includingStudents = false)
        {
            var disciplines = Find(filter);
            List<DisciplineResponce> result = new List<DisciplineResponce>();
            disciplines.ForEach(d =>
            {
                var disciplineResponce = new DisciplineResponce(d);
                //List<Cathedra> subscriberCathedras = new List<Cathedra>();
                List<User> students = new List<User>();

                var lecturer = _userRepo.Find(SearchFilter<User>.FilterById(d.LecturerId))?.SingleOrDefault();

                var providerCathedra = _cathedraRepo.Find(SearchFilter<Cathedra>.FilterById(d.ProviderCathedraId))?.SingleOrDefault();

                // todo: fix
                //if (includingStudents)
                //{
                //    students = _userRepo.Find(new SearchFilter<User>
                //    {
                //        OptionList = d.StudentIds.Select(id => new User() { Id = id })
                //    });
                //}

                disciplineResponce.Lecturer = lecturer;
                disciplineResponce.ProviderCathedra = providerCathedra;
                //disciplineResponce.SubscriberCathedras = subscriberCathedras;
                disciplineResponce.Students = students;

                result.Add(disciplineResponce);
            });

            return result;
        }

        public bool TryRegisterStudent(string studentId, string disciplineId)
        {
            var result = false;
            var student = _userRepo.Find(SearchFilter<User>.FilterById(studentId)).SingleOrDefault();

            var discipline = Find(SearchFilter<Discipline>.FilterById(disciplineId)).SingleOrDefault();

            if (student.Roles.Contains(Role.Student))
            {
                var studentDiscIds = student.Disciplines.Select(el => el.Id);
                var disciplines = Find(SearchFilter<Discipline>.FilterByIds(studentDiscIds));

                var registerLimt = discipline.DisciplineType == DisciplineType.Socio 
                                                        ? Constants.AmountSocioDisciplines 
                                                        : Constants.AmountSpecialDisciplines;

                if (disciplines.Count(d => d.DisciplineType == discipline.DisciplineType) < registerLimt &&
                    !student.Disciplines.Any(el => el.Id == disciplineId))
                {
                    if (student.Disciplines == null)
                    {
                        student.Disciplines = new List<DisciplineModel>();
                    }
                    student.Disciplines.Add(new DisciplineModel { Id = disciplineId });

                    this.Update(disciplineId, discipline);
                    _userRepo.Update(studentId, student);
                    result = true;
                }
            }
            return result;
        }

        public bool UnregisterStudent(string studentId, string disciplineId)
        {
            var student = _userRepo.Find(SearchFilter<User>.FilterById(studentId)).SingleOrDefault();
            var discipline = Find(SearchFilter<Discipline>.FilterById(disciplineId)).SingleOrDefault();

            //add date validation
            student.Disciplines.Remove(
                student.Disciplines.SingleOrDefault(el => el.Id == disciplineId)
                );
            try
            {
                _userRepo.Update(studentId, student);
                Update(disciplineId, discipline);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
