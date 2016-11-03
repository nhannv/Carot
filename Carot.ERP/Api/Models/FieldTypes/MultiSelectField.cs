using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Carot.ERP.Api.Models
{
	public class InputMultiSelectField : InputField
	{
		[JsonProperty(PropertyName = "fieldType")]
		public static FieldType FieldType { get { return FieldType.MultiSelectField; } }

		[JsonProperty(PropertyName = "defaultValue")]
		public IEnumerable<string> DefaultValue { get; set; }

		[JsonProperty(PropertyName = "options")]
		public List<MultiSelectFieldOption> Options { get; set; }
	}

	[Serializable]
	public class MultiSelectField : Field
	{
		[JsonProperty(PropertyName = "fieldType")]
		public static FieldType FieldType { get { return FieldType.MultiSelectField; } }

		[JsonProperty(PropertyName = "defaultValue")]
		public IEnumerable<string> DefaultValue { get; set; }

		[JsonProperty(PropertyName = "options")]
		public List<MultiSelectFieldOption> Options { get; set; }
	}

	[Serializable]
	public class MultiSelectFieldOption
	{
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "value")]
		public string Value { get; set; }

		public MultiSelectFieldOption()
		{

		}

		public MultiSelectFieldOption(string key, string value)
		{
			Key = key;
			Value = value;
		}

		public MultiSelectFieldOption(MultiSelectFieldOption option) : this(option.Key, option.Value)
		{
		}
	}
}