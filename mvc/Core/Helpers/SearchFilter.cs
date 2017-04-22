
namespace Core.Helpers
{
    public class BaseSearchFilter<TEntity>
    {
        public TEntity Query { get; set; }
    }

    public class ExtendedSearchFilter<TEntity> : BaseSearchFilter<TEntity>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
