using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class Lockout
    {
        [JsonProperty("failureCount", NullValueHandling = NullValueHandling.Ignore)]
        public int FailureCount { get; set; }

        [JsonProperty("durationSeconds", NullValueHandling = NullValueHandling.Ignore)]
        public int DurationSeconds { get; set; }
    }
}