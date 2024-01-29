namespace Rendezvous
{
    internal class RendezvousManager
    {
        const int MaxRetry = 8;

        public readonly IStorage _storage;

        public RendezvousManager(IStorage storage)
        {
            _storage = storage;
        }

        public async Task Create(RendezvousCreate rendezvousCreate)
        {
            var (state, success) = rendezvousCreate.Create();

            if (!success)
            {
                throw new Exception("create failed");
            }

            await _storage.CreateAsync(GetBlobName(state.Id), state);
        }

        public async Task UpdateAsync(RendezvousUpdate rendezvousUpdate)
        {
            var blobName = GetBlobName(rendezvousUpdate.Id);

            for (var i = 0; i < MaxRetry; i++)
            {
                var (state, eTag) = await _storage.LoadAsync<RendezvousState>(blobName);

                var success = rendezvousUpdate.Apply(state);

                if (!success)
                {
                    throw new Exception("failed to apply");
                }

                var matched = await _storage.SaveAsync(blobName, state, eTag);

                if (matched)
                {
                    return;
                }
            }

            return;
        }

        public async Task Delete(string id)
        {
            await _storage.DeleteAsync(GetBlobName(id));
        }

        public async Task<RendezvousState> Read(string id)
        {
            var (state, _) = await _storage.LoadAsync<RendezvousState>(GetBlobName(id));
            return state;
        }

        public static string GetBlobName(string id) => $"{id}";
    }
}
