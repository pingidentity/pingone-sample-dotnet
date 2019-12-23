using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class Name
    {
        [JsonProperty("given", NullValueHandling = NullValueHandling.Ignore)]
        public string Given { get; set; }

        [JsonProperty("family", NullValueHandling = NullValueHandling.Ignore)]
        public string Family { get; set; }
    }
}
