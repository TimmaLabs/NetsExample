using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Timma.Print
{
    public class Header
    {
        private static readonly string VERSION = "1.00";

        [JsonProperty("ver")]
        public readonly string version = VERSION;

        [JsonProperty("rows")]
        public List<Row> rows { get; set; }
    }
}
