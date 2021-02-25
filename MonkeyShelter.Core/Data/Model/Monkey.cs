using System;
using System.Text.Json.Serialization;

namespace MonkeyShelter.Data.Model
{
    public class Monkey : Entity<string>
    {
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
