using System.Collections.Generic;
using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class PasswordPolicy
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
        public Environment Environment { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("excludesProfileData", NullValueHandling = NullValueHandling.Ignore)]
        public bool ExcludesProfileData { get; set; }

        [JsonProperty("notSimilarToCurrent", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotSimilarToCurrent { get; set; }

        [JsonProperty("excludesCommonlyUsed", NullValueHandling = NullValueHandling.Ignore)]
        public bool ExcludesCommonlyUsed { get; set; }

        [JsonProperty("maxAgeDays", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxAgeDays { get; set; }

        [JsonProperty("minAgeDays", NullValueHandling = NullValueHandling.Ignore)]
        public int MinAgeDays { get; set; }

        [JsonProperty("maxRepeatedCharacters", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxRepeatedCharacters { get; set; }

        [JsonProperty("minUniqueCharacters", NullValueHandling = NullValueHandling.Ignore)]
        public int MinUniqueCharacters { get; set; }

        [JsonProperty("history", NullValueHandling = NullValueHandling.Ignore)]
        public History History { get; set; }

        [JsonProperty("lockout", NullValueHandling = NullValueHandling.Ignore)]
        public Lockout Lockout { get; set; }

        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public Length Length { get; set; }

        [JsonProperty("minCharacters", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, int> MinCharacters { get; set; }

        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public bool Default { get; set; }
    }
}