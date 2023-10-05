using TrainRouteManager.Domain;

namespace TrainRouteManager.Services
{
    public interface IRouteCreatorService
    {
        Task<TrainRoute> CreateAsync(string name, int initialSeats);
    }
}
