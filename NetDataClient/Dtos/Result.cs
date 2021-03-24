using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedBoarder.NetDataClient.Dtos
{
    public class Result
    {
        [JsonPropertyName("labels")]
        public IList<string>? Labels { get; set; }

        [JsonPropertyName("data")]
        public IList<IList<double>>? Data { get; set; }
    }
}