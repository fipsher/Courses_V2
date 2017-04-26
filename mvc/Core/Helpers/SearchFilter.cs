using System.Collections.Generic;

namespace Core.Helpers
{
    public class SearchFilter<TEntity>
    {
        public IEnumerable<TEntity> Query { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public static SearchFilter<TEntity> Default => new SearchFilter<TEntity> { Take = 10, Skip = 0 };
        public static SearchFilter<TEntity> Empty => new SearchFilter<TEntity>();
    }
}
