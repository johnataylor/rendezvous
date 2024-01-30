namespace Rendezvous
{
    internal static class RendezvousExtensions
    {
        public static (RendezvousState state, bool success) Create(this RendezvousCreate rendezvousCreate)
        {
            return (new RendezvousState { Id = $"ren_{Guid.NewGuid()}", Items = rendezvousCreate.Items }, true);
        }
        public static bool Apply(this RendezvousUpdate rendezvousUpdate, RendezvousState state)
        {
            // basically a super simplistic patch implementation that doesn't a

            var newItems = new List<RendezvousItem>();
            var newItemsLookup = new HashSet<string>();

            // remove items

            var removeLookup = new HashSet<string>(rendezvousUpdate.ItemsToRemove);

            foreach (var item in state.Items)
            {
                if (!removeLookup.Contains(item.Id))
                {
                    newItems.Add(item);
                    newItemsLookup.Add(item.Id);
                }
            }

            // add items

            foreach (var item in state.Items)
            {
                if (!newItemsLookup.Contains(item.Id))
                {
                    newItems.Add(item);
                    newItemsLookup.Add(item.Id);
                }
            }

            state.Items = newItems.ToArray();

            return true;
        }
    }
}
