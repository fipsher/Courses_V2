using Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Core.Entities
{
    public class Entity : IEntity
    {
        [BsonId]
        [JsonProperty("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
