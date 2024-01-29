using Azure;

namespace Rendezvous
{
    internal interface IStorage
    {
        Task<(T state, ETag eTag)> LoadAsync<T>(string blobName);

        Task CreateAsync<T>(string blobName, T state);

        Task<bool> SaveAsync<T>(string blobName, T state, ETag eTag);

        Task DeleteAsync(string blobName);
    }
}
