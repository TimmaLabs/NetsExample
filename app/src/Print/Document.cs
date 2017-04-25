using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System;

namespace NetsExample.Print
{
    public class Document
    {
        internal static readonly int MAX_BITMAP_WIDTH = 383;

        public Document(params Row[] rows)
        {
            Header = new Header();
            Header.rows = rows.ToList();
        }

        [JsonProperty("printmsg")]
        public Header Header { get; set; }
    }
}
