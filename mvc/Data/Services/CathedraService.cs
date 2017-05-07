using Core.Entities;
using Core.Interfaces.Services;
using Core.Interfaces;
using Core.Helpers;
using System.Linq;

namespace Data.Services
{
    class CathedraService : BaseService<Cathedra>, ICathedraService
    {
        private readonly IRepository<Discipline> _disciplineRepo;

        public CathedraService(IRepositoryBootstrapper repositoryStrategy) : base(repositoryStrategy)
        {
            _disciplineRepo = (IRepository<Discipline>)repositoryStrategy[typeof(Discipline)];
        }
        //public override void Update(string id, Cathedra entity)
        //{
        //    var disciplines = _disciplineRepo.Find(SearchFilter<Discipline>.FilterByIds(
        //                            entity.CathedraSubscribers.Select(cs => cs.CathedraId).ToList()));

            

        //    base.Update(id, entity);
        //}
    }
}
