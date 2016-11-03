﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Carot.ERP.Api.Models
{
	[Serializable]
	public class QueryObject
	{
        [JsonProperty(PropertyName = "queryType")]
        public QueryType QueryType { get; set; }

        [JsonProperty(PropertyName = "fieldName")]
        public string FieldName { get; set; }

        [JsonProperty(PropertyName = "fieldValue")]
        public object FieldValue { get; set; }

		[JsonProperty(PropertyName = "fieldValue")]
		public QueryObjectRegexOperator RegexOperator { get; set; }

		[JsonProperty(PropertyName = "subQueries")]
        public List<QueryObject> SubQueries { get; set; }
	}

	public enum QueryObjectRegexOperator 
	{
		MatchCaseSensitive,
		MatchCaseInsensitive,
		DontMatchCaseSensitive,
		DontMatchCaseInsensitive,
	}
}