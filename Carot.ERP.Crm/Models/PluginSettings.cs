using Newtonsoft.Json;

namespace Carot.ERP.Crm.Models
{
    public class PluginSettings
    {
		[JsonProperty(PropertyName = "version")]
		public int Version { get; set; }
    }
}
