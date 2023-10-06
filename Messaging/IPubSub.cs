namespace Messaging
{
    public interface IPubSub<T> where T : class
    {
        Task SubscribeAsync(string exchangeName, string topic, Func<T, Task> handler);
        Task PublishAsync(string exchangeName, string topic, T message);
    }
}