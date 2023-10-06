using Messaging;
using TicketingSystem.Domain;

namespace TicketingSystem.HostedServices
{
    public class PaymentMessageProcessor : IHostedService
    {
        private readonly IPubSub<Purchase> _pubSub;

        public PaymentMessageProcessor(IPubSub<Purchase> pubSub)
        {
            _pubSub = pubSub;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _pubSub.SubscribeAsync("trainTickets", "completedPurchases", FinalizePurchase);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        async Task FinalizePurchase(Purchase purchase)
        {
            Console.WriteLine($"New purchase made by {purchase.PassengerName} on route {purchase.RouteName} for {purchase.NumberOfSeats} seats");
        }
    }
}
