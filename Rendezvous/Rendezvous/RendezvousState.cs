using System.Text.Json.Serialization;

namespace Rendezvous
{
    internal class RendezvousState
    {
        [JsonPropertyName("items")]
        public required RendezvousItem[] Items { get; set; } 
    }
}
