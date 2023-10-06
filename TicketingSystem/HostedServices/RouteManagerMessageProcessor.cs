using Messaging;
using TicketingSystem.Domain;

namespace TicketingSystem.HostedServices
{
    public class RouteManagerMessageProcessor : IHostedService
    {
        List<TrainRoute> _routes = new List<TrainRoute>();
        private readonly IPubSub<TrainRoute> _pubSub;

        public RouteManagerMessageProcessor(IPubSub<TrainRoute> pubSub)
        {
            _pubSub = pubSub;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _pubSub.SubscribeAsync("trainTickets", "addedRoutes", AddNewRoute);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        async Task AddNewRoute(TrainRoute route)
        {
            _routes.Add(route);
            Console.WriteLine($"New route added: {route.Name}");
        }
    }
}
