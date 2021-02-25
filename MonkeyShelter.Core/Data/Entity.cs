using System.Text.Json.Serialization;

namespace MonkeyShelter.Data
{
    public class Entity<TKey>: IEntity<TKey>
    {
        [JsonPropertyName("_id")]
        public TKey Id { get; set; }

        object IEntity.Id => this.Id;
    }
}