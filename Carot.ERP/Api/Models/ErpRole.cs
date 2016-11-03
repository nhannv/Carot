﻿using Newtonsoft.Json;
using System;

namespace Carot.ERP.Api.Models
{
	[Serializable]
	public class ErpRole
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

} 