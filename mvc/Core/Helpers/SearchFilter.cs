using System.Collections.Generic;

namespace Core.Helpers
{
    public class SearchFilter<TEntity>
    {
        public IEnumerable<TEntity> Query { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }
}
