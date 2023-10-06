using Messaging;
using TrainRouteManager.Domain;

namespace TrainRouteManager.Services
{
    public class RouteCreatorService : IRouteCreatorService
    {
        private static List<TrainRoute> _routes = new List<TrainRoute>();
        private readonly IPubSub<TrainRoute> _pubSub;

        public RouteCreatorService(IPubSub<TrainRoute> pubSub)
        {
            _pubSub = pubSub;
        }

        public async Task<TrainRoute> CreateAsync(string name, int initialSeats)
        {
            var trainRoute = new TrainRoute(name, initialSeats);

            _routes.Add(trainRoute);

            await _pubSub.PublishAsync("trainTickets", "addedRoutes", trainRoute);

            return trainRoute;
        }
    }
}
