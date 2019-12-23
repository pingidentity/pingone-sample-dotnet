using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class Length
    {
        [JsonProperty("min", NullValueHandling = NullValueHandling.Ignore)]
        public int Min { get; set; }

        [JsonProperty("max", NullValueHandling = NullValueHandling.Ignore)]
        public int Max { get; set; }
    }
}