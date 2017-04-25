using System;
using System.Collections.Generic;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using Core.Interfaces.Services;
using System.Linq;
using Core.Responces;

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

                var providerCathedra = _cathedraRepo.Find(new SearchFilter<Cathedra>
                {
                    Query = new[] { new Cathedra() { Id = d.ProviderCathedraId } }
                })?.SingleOrDefault();

                if (includingSubscriberCathedras)
                {
                    subscriberCathedras = _cathedraRepo.Find(new SearchFilter<Cathedra>
                    {
                        Query = d.SubscriberCathedraIds.Select(id => new Cathedra() { Id = id})
                    });
                }

                if (includingStudents)
                {
                    students = _userRepo.Find(new SearchFilter<User>
                    {
                        Query = d.StudentIds.Select(id => new User() { Id = id })
                    });
                }

                disciplineResponce.ProviderCathedra = providerCathedra;
                disciplineResponce.SubscriberCathedras = subscriberCathedras;
                disciplineResponce.Students = students;

                result.Add(disciplineResponce);
            });

            return result;
        }
    }
}
