using Newtonsoft.Json;

namespace Carot.ERP.Api.Models
{
    public class QueryCountResponse : BaseResponseModel
    { 
        [JsonProperty(PropertyName = "object")]
        public long Object { get; set; }
	}
}
