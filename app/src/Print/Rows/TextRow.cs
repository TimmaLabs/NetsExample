using Newtonsoft.Json;

namespace NetsExample.Print
{
    class TextRow : Row
    {
        public TextRow(string content = "NetsExample")
        {
            Data = content;
        }

        [JsonProperty("font")]
        public string Font { get; set; } = "normal";

        [JsonProperty("w")]
        public string W { get; set; } = "fix";

        [JsonProperty("align")]
        public string Align { get; set; } = "left";

        [JsonProperty("reverse")]
        public int Reverse { get; set; } = 0;

        [JsonProperty("blank")]
        public int Blank { get; set; } = 0;

        public override string Type { get; } = "txt";
    }
}
