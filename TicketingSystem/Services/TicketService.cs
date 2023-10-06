using Messaging;
using TicketingSystem.Domain;

namespace TicketingSystem.Services
{
    public class TicketService : ITicketService
    {
        private static List<Purchase> _purchases = new List<Purchase>();
        private readonly IPubSub<Purchase> _pubSub;

        public TicketService(IPubSub<Purchase> pubSub) 
        {
            _pubSub = pubSub;
        }
        public async Task InitiatePurchase(Purchase purchase)
        {
            purchase.State = Enums.PurchaseState.Pending;

            _purchases.Add(purchase);

            await _pubSub.PublishAsync("trainTickets", "initiatedPurchases", purchase);
        }
    }
}
