using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class Lifecycle
    {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
