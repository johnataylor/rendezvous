using System.Text.Json.Serialization;

namespace Rendezvous
{
    internal class RendezvousUpdate
    {
        public required string Id { get; set; }

        [JsonPropertyName("items")]
        public required RendezvousItem[] ItemsToAdd { get; set; }

        [JsonPropertyName("items")]
        public required RendezvousItem[] ItemsToRemove { get; set; }

    }
}
