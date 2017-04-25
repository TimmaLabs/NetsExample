using BBS.BAXI;
using Newtonsoft.Json;

namespace NetsExample.Terminal
{
    class TerminalInfo
    {
        public TerminalInfo(BaxiCtrl terminal)
        {
            Id = terminal.TerminalID;
            Type = terminal.TermType;
            SoftwareVersion = terminal.TerminalSwVersion;
            COMPort = terminal.ComPort;
            BaudRate = terminal.BaudRate;
            Host = string.Format("{0}:{1}", terminal.HostIpAddress, terminal.HostPort);
            LogFilePath = terminal.LogFilePath;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("softwareVersion")]
        public string SoftwareVersion { get; set; }

        [JsonProperty("COMPort")]
        public int COMPort { get; set; }

        [JsonProperty("baudRate")]
        public int BaudRate { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("logFilePath")]
        public string LogFilePath { get; set; }
    }
}
