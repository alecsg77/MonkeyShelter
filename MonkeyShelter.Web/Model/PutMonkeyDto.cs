using System;
using System.Text.Json.Serialization;

namespace MonkeyShelter.Web.Model
{
    public class PutMonkeyDto
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
        [JsonPropertyName("eyeColor")]
        public string EyeColor { get; set; }
        [JsonPropertyName("species")]
        public string Species { get; set; }
        [JsonPropertyName("registered")]
        public DateTime Registered { get; set; }
        [JsonPropertyName("favoriteFruit")]
        public string FavoriteFruit { get; set; }
    }
}