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
            // add items

            // remove items

            return true;
        }
    }
}
