using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Rendezvous
{
    internal class Storage : IStorage
    {
        private const string ContainerName = "testing";
        private readonly BlobContainerClient _containerClient;

        public Storage(IConfiguration config)
        {
            var connectionString = config["BlobConnectionString"];
            _containerClient = new BlobContainerClient(connectionString, ContainerName);
        }

        public async Task<(T state, ETag eTag)> LoadAsync<T>(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            var response = await blobClient.DownloadContentAsync();
            var json = Encoding.UTF8.GetString(response.Value.Content);
            var state = JsonSerializer.Deserialize<T>(json) ?? throw new Exception($"unable to derserialize content to '{typeof(T).Name}'");
            var originalETag = response.Value.Details.ETag;
            return (state, originalETag);
        }

        public async Task CreateAsync<T>(string blobName, T state)
        {
            await _containerClient.UploadBlobAsync(blobName, new BinaryData(state));
        }

        public async Task<bool> SaveAsync<T>(string blobName, T state, ETag eTag)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);

            var blobUploadOptions = new BlobUploadOptions
            {
                Conditions = new BlobRequestConditions()
                {
                    IfMatch = eTag
                }
            };

            try
            {
                BlobContentInfo blobContentInfo = await blobClient.UploadAsync(new BinaryData(state), blobUploadOptions);
                return true;
            }
            catch (RequestFailedException ex)
            {

                return false;
            }
        }
        public async Task DeleteAsync(string blobName)
        {
            await _containerClient.DeleteBlobAsync(blobName);
        }
    }
}
