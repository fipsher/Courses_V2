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


                if (includingStudents)
                {
                    students = _userRepo.Find(new SearchFilter<User>
                    {
                        OptionList = d.StudentIds.Select(id => new User() { Id = id })
                    });
                }

                disciplineResponce.Lecturer = lecturer;
                disciplineResponce.ProviderCathedra = providerCathedra;
                //disciplineResponce.SubscriberCathedras = subscriberCathedras;
                disciplineResponce.Students = students;

                result.Add(disciplineResponce);
            });

            return result;
        }

        public bool RegisterStudent(string studentId, string disciplineId)
        {
            var result = false;
            var student = _userRepo.Find(new SearchFilter<User>()
            {
                OptionList = new[] { new User() { Id = studentId } }
            }).SingleOrDefault();

            var discipline = Find(new SearchFilter<Discipline>()
            {
                OptionList = new[] { new Discipline() { Id = disciplineId } }
            }).SingleOrDefault();

            if (student.Roles.Contains(Role.Student))
            {
                var disciplines = Find(new SearchFilter<Discipline>()
                {
                    OptionList = student.Disciplines.Select(rd => new Discipline { Id = rd.DisciplineId })
                });
                if (disciplines.Count(d => d.DisciplineType == DisciplineType.Socio) < Constants.AmountSocioDisciplines &&
                    disciplines.Count(d => d.DisciplineType == DisciplineType.Special) < Constants.AmountSpecialDisciplines &&
                    !student.Disciplines.Any(rd => rd.DisciplineId == disciplineId))
                {
                    discipline.StudentIds.Add(studentId);
                    student.Disciplines.Add(new DisciplineRegister { DisciplineId = disciplineId, DateTime = DateTime.UtcNow });
                    this.Update(disciplineId, discipline);
                    _userRepo.Update(studentId, student);
                    result = true;
                }
            }
            return result;
        }

        public bool UnregisterStudent(string studentId, string disciplineId)
        {
            var student = _userRepo.Find(new SearchFilter<User>()
            {
                OptionList = new[] { new User() { Id = studentId } }
            }).SingleOrDefault();

            var discipline = Find(new SearchFilter<Discipline>()
            {
                OptionList = new[] { new Discipline() { Id = disciplineId } }
            }).SingleOrDefault();
            var registerDiscipline = student.Disciplines.SingleOrDefault(rd => rd.DisciplineId == disciplineId);
            //_settingRepo.Find(new SearchFilter<Setting>
            //{
            //    OptionList = new[] {new Setting { Id = } }
            //});
            //if (DateTime.UtcNow >= Iwe)
            student.Disciplines.Remove(registerDiscipline);
            discipline.StudentIds?.Remove(studentId);
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

        //public override void Add(Discipline entity)
        //{
        //    var cathedra = _cathedraRepo.Find(SearchFilter<Cathedra>.FilterById(entity.ProviderCathedraId)).SingleOrDefault();
        //    var subscribers = cathedra.CathedraSubscribers
        //                              .Where(cs => cs.Semestr == entity.Semester)
        //                              .Select(cs => cs.CathedraId)
        //                              .ToList();

        //    entity.SubscriberCathedraIds = subscribers;
        //    base.Add(entity);
        //}
    }
}
