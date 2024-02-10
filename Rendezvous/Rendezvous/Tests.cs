using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Rendezvous
{
    internal static class Tests
    {
        private const string ContainerName = "rendezvous";

        private const string RunId = "console_testing";

        public async static Task Test0()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var rendezvous = new RendezvousState
            {
                Id = "blah",
                Items = [ new RendezvousItem { Id = "1" } ]
            };

            var storage = new Storage(config);

            await storage.CreateAsync(GetBlobName(RunId), rendezvous);

            //await BlobStorageHelpers.DownloadAsync<Rendezvous>(_containerClient, GetBlobName(runId));

            var update = new RendezvousUpdate
            {
                Id = "blah",
                ItemsToAdd = [new RendezvousItem { Id = "2" }, new RendezvousItem { Id = "3" }],
                ItemsToRemove = ["1"]
            };


        }

        private static string GetBlobName(string runId) => $"{runId}_rendezvous";
    }
}
