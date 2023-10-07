namespace Messaging
{
    public interface IPubSub<T> where T : class
    {
        Task SubscribeAsync(string exchangeName, string routingKey, Func<T, Task> handler);
        Task PublishAsync(string exchangeName, string routingKey, T message);
    }
}