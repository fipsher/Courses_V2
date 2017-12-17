using Core.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using static Core.Enums.Enums;

namespace Core.Responces
{
    public class DisciplineCount
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        public int Count { get; set; }
    }
}
