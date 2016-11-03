using Newtonsoft.Json;

namespace Carot.ERP.Api.Models
{
    public class QueryResponse : BaseResponseModel
    {
		public QueryResponse() {
			Object = new QueryResult();
		}

		[JsonProperty(PropertyName = "object")]
        public QueryResult Object { get; set; }
	}
}
