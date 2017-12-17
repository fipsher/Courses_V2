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
using LightCaseClient;
using Newtonsoft.Json;

namespace Data.Services
{
    internal class DisciplineService : BaseService<Discipline>, IDisciplineService
    {
        private readonly IRepository<Cathedra> _cathedraRepo;
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Setting> _settingRepo;

        public DisciplineService(IRepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
            _cathedraRepo = (IRepository<Cathedra>)repositoryStrategy[typeof(Cathedra)];
            _groupRepo = (IRepository<Group>)repositoryStrategy[typeof(Group)];
            _userRepo = (IRepository<User>)repositoryStrategy[typeof(User)];
            _settingRepo = (IRepository<Setting>)repositoryStrategy[typeof(Setting)];
        }

        public List<GroupDisciplineModel> FindDisciplineResponse(SearchFilter<Discipline> filter)
        {
            var disciplines = Repository.Find(filter);
            List<User> lecturers = new List<User>();
            List<Cathedra> cathedras = new List<Cathedra>();
            if (disciplines.Any())
            {
                var userIds = disciplines.Select(el => el.LecturerId);
                lecturers = _userRepo.Find(SearchFilter<User>.FilterByIds(userIds));

                var cathedraIds = disciplines.Select(el => el.ProviderCathedraId);
                cathedras = _cathedraRepo.Find(SearchFilter<Cathedra>.FilterByIds(cathedraIds));
            }

            var disciplinesCount = GetDisciplinesCount(disciplines.Select(el => el.Id));
            return disciplines.Select(d => new GroupDisciplineModel
            {
                Id = d.Id,
                Name = d.Name,
                DisciplineType = d.DisciplineType,
                Semester = d.Semester,
                Description = d.Description,
                Lecturer = lecturers.SingleOrDefault(el => el.Id == d.LecturerId)?.UserName,
                ProviderCathedra = cathedras.SingleOrDefault(el => el.Id == d.ProviderCathedraId)?.Name,
                Count = disciplinesCount.SingleOrDefault(el => el.Id == d.Id)?.Count ?? 0
            }).ToList();
        }

        public bool TryRegisterStudent(string studentId, string disciplineId)
        {
            var result = false;
            var student = _userRepo.Find(SearchFilter<User>.FilterById(studentId)).SingleOrDefault();
            var group = _groupRepo.Find(SearchFilter<Group>.FilterById(student.GroupId)).SingleOrDefault();

            var discipline = Find(SearchFilter<Discipline>.FilterById(disciplineId)).SingleOrDefault();

            if (student.Roles.Contains(Role.Student) && discipline != null)
            {
                var studentDiscIds = student.Disciplines.Select(el => el.Id);
                var disciplines = studentDiscIds.Any()
                    ? Find(SearchFilter<Discipline>.FilterByIds(studentDiscIds))
                    : new List<Discipline>();

                var registerLimt = group.DisciplineConfiguration
                    .Single(el => el.Semester % 2 == discipline.Semester % 2
                                        && el.DisciplineType == discipline.DisciplineType)
                    .RequiredAmount;

                if (disciplines.Count(d => d.DisciplineType == discipline.DisciplineType 
                && discipline.Semester == d.Semester) < registerLimt &&
                    student.Disciplines.All(el => el.Id != disciplineId))
                {
                    student.Disciplines.Add(new DisciplineModel { Id = disciplineId });

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

        private List<DisciplineCount> GetDisciplinesCount(IEnumerable<string> disciplineIds)
        {
            return GenericProxies.RestPost<List<DisciplineCount>, IEnumerable<string>>($"{Repository.ApiUrl}/findStudentsCountForDisciplineId", disciplineIds);
        }
    }
}
