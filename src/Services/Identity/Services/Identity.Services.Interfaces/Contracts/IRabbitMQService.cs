namespace Identity.Services.Interfaces.Helpers.Rabbit
{
    public interface IRabbitMQService
    {
        void SendMessageToQueue(object message);
    }
}
