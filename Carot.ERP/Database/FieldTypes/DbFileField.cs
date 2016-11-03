using Newtonsoft.Json;

namespace Carot.ERP.Database
{
	public class DbFileField : DbBaseField
    {
		[JsonProperty(PropertyName = "default_value")]
		public string DefaultValue { get; set; }
    }
}