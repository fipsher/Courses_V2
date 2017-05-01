using Core.Entities;
using Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Helpers
{
    public class SearchFilter<TEntity> where TEntity :  Entity, new()
    {
        [JsonProperty("optionList")]
        public IEnumerable<TEntity> OptionList { get; set; }
        [JsonProperty("take")]
        public int Take { get; set; }
        [JsonProperty("skip")]
        public int Skip { get; set; }

        public static SearchFilter<TEntity> Default => new SearchFilter<TEntity> { Take = 10, Skip = 0 };
        public static SearchFilter<TEntity> Empty => new SearchFilter<TEntity>();

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
