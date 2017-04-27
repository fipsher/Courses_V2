using System.Collections.Generic;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using System.Linq;
using Core.Responces;
using System;
using static Core.Enums.Enums;
using Core;

namespace Data.Services
{
    internal class DisciplineService : BaseService<Discipline>, IDisciplineService
    {
        private readonly IRepository<Cathedra> _cathedraRepo;
        private readonly IRepository<User> _userRepo;
        public DisciplineService(IRepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
            _cathedraRepo = (IRepository<Cathedra>)repositoryStrategy[typeof(Cathedra)];
            _userRepo = (IRepository<User>)repositoryStrategy[typeof(User)];
        }

        public List<DisciplineResponce> FindDisciplineResponse(SearchFilter<Discipline> filter, bool includingSubscriberCathedras = false, bool includingStudents = false)
        {
            var disciplines = Find(filter);
            List<DisciplineResponce> result = new List<DisciplineResponce>();
            disciplines.ForEach(d =>
            {
                var disciplineResponce = new DisciplineResponce(d);
                List<Cathedra> subscriberCathedras = new List<Cathedra>();
                List<User> students = new List<User>();

                var lecturer = _userRepo.Find(new SearchFilter<User>
                {
                    Query = new[] { new User() { Id = d.LecturerId } }
                })?.SingleOrDefault();

                var providerCathedra = _cathedraRepo.Find(new SearchFilter<Cathedra>
                {
                    Query = new[] { new Cathedra() { Id = d.ProviderCathedraId } }
                })?.SingleOrDefault();

                if (includingSubscriberCathedras)
                {
                    subscriberCathedras = _cathedraRepo.Find(new SearchFilter<Cathedra>
                    {
                        Query = d.SubscriberCathedraIds.Select(id => new Cathedra() { Id = id })
                    });
                }

                if (includingStudents)
                {
                    students = _userRepo.Find(new SearchFilter<User>
                    {
                        Query = d.StudentIds.Select(id => new User() { Id = id })
                    });
                }

                disciplineResponce.Lecturer = lecturer;
                disciplineResponce.ProviderCathedra = providerCathedra;
                disciplineResponce.SubscriberCathedras = subscriberCathedras;
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
                Query = new[] { new User() { Id = studentId } }
            }).SingleOrDefault();

            var discipline = Find(new SearchFilter<Discipline>()
            {
                Query = new[] { new Discipline() { Id = disciplineId } }
            }).SingleOrDefault();

            if (student.Roles.Contains(Role.Student))
            {
                var disciplines = Find(new SearchFilter<Discipline>()
                {
                    Query = student.ChoosenDisciplineIds.Select(id => new Discipline { Id = id })
                });
                if (disciplines.Count(d => d.DisciplineType == DisciplineType.Socio) < Constants.AmountSocioDisciplines &&
                    disciplines.Count(d => d.DisciplineType == DisciplineType.Special) < Constants.AmountSpecialDisciplines &&
                    !student.ChoosenDisciplineIds.Contains(disciplineId))
                {
                    discipline.StudentIds.Add(studentId);
                    student.ChoosenDisciplineIds.Add(disciplineId);
                    this.Add(discipline);
                    _userRepo.Update(student);
                    result = true;
                }
            }
            return result;
        }
    }
}
