using System.Text.Json.Serialization;

namespace RedBoarder.NetDataClient.Dtos
{
    public class Labels
    {

        [JsonPropertyName("_is_master")]
        public string? IsMaster { get; set; }

        [JsonPropertyName("_container")]
        public string? Container { get; set; }

        [JsonPropertyName("_virtualization")]
        public string? Virtualization { get; set; }

        [JsonPropertyName("_architecture")]
        public string? Architecture { get; set; }

        [JsonPropertyName("_kernel_version")]
        public string? Kernel_version { get; set; }

        [JsonPropertyName("_os_version")]
        public string? OsVersion { get; set; }

        [JsonPropertyName("_os_name")]
        public string? OsName { get; set; }
    }
}