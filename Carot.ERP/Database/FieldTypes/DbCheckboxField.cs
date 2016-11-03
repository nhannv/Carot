using Newtonsoft.Json;


namespace Carot.ERP.Database
{
    public class DbCheckboxField : DbBaseField
    {
		[JsonProperty(PropertyName = "default_value")]
		public bool DefaultValue { get; set; }
    }
}