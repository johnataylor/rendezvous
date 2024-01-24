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

            var connectionString = config["BlobConnectionString"];
            var containerClient = new BlobContainerClient(connectionString, ContainerName);


            var rendezvous = new RendezvousState
            {
                Items =
                [
                    new RendezvousItem { Id = "1" }
                ]
            };

            var blobClient = containerClient.GetBlobClient(GetBlobName(RunId));
            await blobClient.UploadAsync(new BinaryData(rendezvous), true);




            //await BlobStorageHelpers.DownloadAsync<Rendezvous>(_containerClient, GetBlobName(runId));


        }

        private static string GetBlobName(string runId) => $"{runId}_rendezvous";
    }
}
