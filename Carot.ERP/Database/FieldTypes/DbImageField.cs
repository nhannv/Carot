﻿using Newtonsoft.Json;

namespace Carot.ERP.Database
{
	public class DbImageField : DbBaseField
    {
		[JsonProperty(PropertyName = "default_value")]
		public string DefaultValue { get; set; }
    }
}