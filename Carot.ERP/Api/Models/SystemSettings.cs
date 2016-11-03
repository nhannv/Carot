using Newtonsoft.Json;
using System;
using Carot.ERP.Database;

namespace Carot.ERP.Api.Models
{
    public class SystemSettings
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "version")]
        public int Version { get; set; }

        public SystemSettings()
        {

        }

        public SystemSettings(DbSystemSettings settings)
        {
            Id = settings.Id;
            Version = settings.Version;
        }
    }
}
