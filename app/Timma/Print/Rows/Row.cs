using Newtonsoft.Json;

namespace Timma.Print
{
    public abstract class Row
    {
        [JsonProperty("type")]
        public abstract string Type { get; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}