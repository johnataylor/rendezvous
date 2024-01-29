using System.Text.Json.Serialization;

namespace Rendezvous
{
    internal class RendezvousCreate
    {
        [JsonPropertyName("items")]
        public required RendezvousItem[] Items { get; set; }
    }
}
