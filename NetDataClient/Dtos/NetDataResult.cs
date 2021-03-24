using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RedBoarder.NetDataClient.Dtos
{
    public class NetDataResult
    {
        [JsonPropertyName("api")]
        public int? Api { get; set; }

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("labels")]
        public Labels? Labels { get; set; }

        [JsonPropertyName("view_update_every")]
        public int? ViewUpdateEvery { get; set; }

        [JsonPropertyName("update_every")]
        public int? UpdateEvery { get; set; }

        [JsonPropertyName("first_entry")]
        public int? FirstEntry { get; set; }

        [JsonPropertyName("last_entry")]
        public int? LastEntry { get; set; }

        [JsonPropertyName("before")]
        public int? Before { get; set; }

        [JsonPropertyName("after")]
        public int? After { get; set; }

        [JsonPropertyName("dimension_names")]
        public IList<string>? DimensionNames { get; set; }

        [JsonPropertyName("dimension_ids")]
        public IList<string>? DimensionIds { get; set; }

        [JsonPropertyName("latest_values")]
        public IList<double>? LatestValues { get; set; }

        [JsonPropertyName("view_latest_values")]
        public IList<double>? ViewLatestValues { get; set; }

        [JsonPropertyName("dimensions")]
        public int? Dimensions { get; set; }

        [JsonPropertyName("points")]
        public int? Points { get; set; }

        [JsonPropertyName("format")]
        public string? Format { get; set; }

        [JsonPropertyName("result")]
        public Result? Result { get; set; }

        [JsonPropertyName("min")]
        public double? Min { get; set; }

        [JsonPropertyName("max")]
        public double? Max { get; set; }
    }
}