using Newtonsoft.Json;

namespace Carot.ERP.Database
{
	public class DbHtmlField : DbBaseField
    {
		[JsonProperty(PropertyName = "default_value")]
		public string DefaultValue { get; set; }
    }
}