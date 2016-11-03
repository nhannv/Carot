using Newtonsoft.Json;
using System.Collections.Generic;

namespace Carot.ERP.Api.Models
{
    public class QueryResult
    {
        [JsonProperty(PropertyName = "fieldsMeta")]
        public List<Field> FieldsMeta { get; set; } 

        [JsonProperty(PropertyName = "data")]
        public List<EntityRecord> Data { get; set; }
    }
}
