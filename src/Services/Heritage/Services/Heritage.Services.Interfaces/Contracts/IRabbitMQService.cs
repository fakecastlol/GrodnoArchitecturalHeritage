namespace Heritage.Services.Interfaces.Contracts
{
    public interface IRabbitMQService
    {
        void SendMessageToQueue(object message);
    }
}
