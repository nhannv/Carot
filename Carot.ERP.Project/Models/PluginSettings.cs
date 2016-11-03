using Newtonsoft.Json;

namespace Carot.ERP.Project.Models
{
    public class PluginSettings
    {
		[JsonProperty(PropertyName = "version")]
		public int Version { get; set; }
    }
}
