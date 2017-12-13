using Core.Interfaces;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class Entity : IEntity
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
