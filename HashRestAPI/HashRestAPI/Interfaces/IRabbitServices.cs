namespace HashRestAPI.Interfaces
{
    public interface IRabbitServices
    {
        void SendMessage<T>(T message);
    }
}
