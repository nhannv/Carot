using Newtonsoft.Json;

namespace Carot.ERP.Database
{
	public class DbSystemSettings : DbDocumentBase
    {
		[JsonProperty(PropertyName = "version")]
		public int Version { get; set; }
    }
}
