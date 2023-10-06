using TicketingSystem.Domain;

namespace TicketingSystem.Services
{
    public interface ITicketService
    {
        Task InitiatePurchase(Purchase purchase);
    }
}
