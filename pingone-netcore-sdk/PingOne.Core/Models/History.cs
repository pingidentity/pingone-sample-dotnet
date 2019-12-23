using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class History
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public int Count { get; set; }

        [JsonProperty("retentionDays", NullValueHandling = NullValueHandling.Ignore)]
        public int RetentionDays { get; set; }
    }
}