namespace hnAPI.Domain.Entities
{
    using System.Text.Json.Serialization;
    public class Story
    {
        [JsonPropertyName("kids")]
        public int[] Kids { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("by")]
        public string By { get; set; } = string.Empty;

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
