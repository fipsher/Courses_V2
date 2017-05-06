using Core.Entities;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public class SearchFilter<TEntity> where TEntity :  Entity, new()
    {
        public IEnumerable<TEntity> OptionList { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public Dictionary<string, object> PrepareForRequest()
        {
            var result = new Dictionary<string, object>
            {
                { "optionList", Constructor.ConstructOptionList(OptionList.ToList()) },
                { "take", Take },
                { "skip", Skip }
            };
            return result;
        }

        public static SearchFilter<TEntity> Empty => new SearchFilter<TEntity> { Take = 10, Skip = 0, OptionList = new List<TEntity>() { new TEntity()} };
        public static SearchFilter<TEntity> FilterById(string id)
        {
            return new SearchFilter<TEntity>
            {
                OptionList = new List<TEntity>
                {
                    new TEntity { Id = id }
                }
            };
        }
    }
}
