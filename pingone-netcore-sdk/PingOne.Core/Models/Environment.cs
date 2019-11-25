using Newtonsoft.Json;

namespace PingOne.Core.Models
{
    public class Environment
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
}
