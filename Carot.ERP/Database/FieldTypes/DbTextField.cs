﻿using Newtonsoft.Json;

namespace Carot.ERP.Database
{
	public class DbTextField : DbBaseField
    {
		[JsonProperty(PropertyName = "default_value")]
		public string DefaultValue { get; set; }

		[JsonProperty(PropertyName = "max_length")]
		public int? MaxLength { get; set; }
    }
}