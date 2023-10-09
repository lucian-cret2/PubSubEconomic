using Messaging;
using PaymentServices;
using RabbitMQ.Client;

List<Purchase> _purchases = new List<Purchase>();

Console.WriteLine("Welcome to the Payment services");

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

PubSub<Purchase> pubSub = new PubSub<Purchase>(channel);
await pubSub.SubscribeAsync("trainTickets", "initiatedPurchases", MakePayment);


Console.WriteLine("Press [enter] to exit");
Console.ReadLine();


async Task MakePayment(Purchase purchase)
{
    _purchases.Add(purchase);

    purchase.State = PurchaseState.Completed;

    await pubSub.PublishAsync("trainTickets", "completedPurchases", purchase);

    Console.WriteLine($"New payment made by {purchase.PassengerName} on route {purchase.RouteName} for {purchase.NumberOfSeats} seats");
}