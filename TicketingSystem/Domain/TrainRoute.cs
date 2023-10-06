namespace TicketingSystem.Domain
{
    public class TrainRoute
    {
        public TrainRoute(string name, int initialSeats)
        {
            Name = name;
            InitialSeats = initialSeats;
        }

        public string Name { get; set; }
        public int InitialSeats { get; set; }
    }
}
