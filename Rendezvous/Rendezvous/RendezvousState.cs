﻿using System.Text.Json.Serialization;

namespace Rendezvous
{
    internal class RendezvousState
    {
        public required string Id { get; set; }

        [JsonPropertyName("items")]
        public required RendezvousItem[] Items { get; set; }
    }
}
