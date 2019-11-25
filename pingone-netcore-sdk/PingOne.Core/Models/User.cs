using System;
using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class User
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public Environment Environment { get; set; }

        [JsonProperty("population", NullValueHandling = NullValueHandling.Ignore)]
        public Population Population { get; set; }

        [JsonProperty("createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Enabled { get; set; } = true;

        [JsonProperty("lifecycle", NullValueHandling = NullValueHandling.Ignore)]
        public Lifecycle Lifecycle { get; set; }

        [JsonProperty("mfaEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool MfaEnabled { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public Name Name { get; set; }

        [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }
    }
}
