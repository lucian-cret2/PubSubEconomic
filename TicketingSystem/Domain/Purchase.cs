using TicketingSystem.Enums;

namespace TicketingSystem.Domain
{
    public class Purchase
    {
        public string PassengerName { get; set; }
        public string RouteName { get; set; }
        public int NumberOfSeats { get; set; }
        public PurchaseState State { get; set; }

    }
}
