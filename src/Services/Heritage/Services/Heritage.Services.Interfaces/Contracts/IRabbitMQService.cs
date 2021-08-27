namespace Heritage.Services.Interfaces.Contracts
{
    public interface IEventBus
    {
        void Publish(object message);
    }
}
