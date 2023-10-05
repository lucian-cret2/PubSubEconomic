using TrainRouteManager.Domain;

namespace TrainRouteManager.Services
{
    public class RouteCreatorService : IRouteCreatorService
    {
        private static List<TrainRoute> _routes = new List<TrainRoute>();

        public async Task<TrainRoute> CreateAsync(string name, int initialSeats)
        {
            var trainRoute = new TrainRoute(name, initialSeats);

            _routes.Add(trainRoute);

            return trainRoute;
        }
    }
}
