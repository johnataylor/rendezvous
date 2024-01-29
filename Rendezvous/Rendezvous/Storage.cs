using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Rendezvous
{
    internal class Storage : IStorage
    {
        private readonly BlobClient _blobClient;

        public Storage(BlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<(T state, ETag eTag)> LoadAsync<T>(string blobName)
        {
            Response<BlobDownloadResult> response = await _blobClient.DownloadContentAsync();
            BlobDownloadResult downloadResult = response.Value;
            string blobContents = downloadResult.Content.ToString();

            ETag originalETag = downloadResult.Details.ETag;

            throw new NotImplementedException();
        }

        public Task CreateAsync<T>(string blobName, T state)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync<T>(string blobName, T state, ETag eTag)
        {
            var blobContent = "blah blah blah";

            BlobUploadOptions blobUploadOptions = new()
            {
                Conditions = new BlobRequestConditions()
                {
                    IfMatch = eTag
                }
            };

            // This call should fail with error code 412 (Precondition Failed)
            BlobContentInfo blobContentInfo =
                await _blobClient.UploadAsync(BinaryData.FromString(blobContent), blobUploadOptions);

            throw new NotImplementedException();
        }
        public Task DeleteAsync(string blobName)
        {
            throw new NotImplementedException();
        }
    }
}
