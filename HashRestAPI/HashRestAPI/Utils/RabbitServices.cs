using HashRestAPI.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;


namespace HashRestAPI.Utils
{
    public class RabbitServices : IRabbitServices
    {
        private readonly string? _queue = System.Configuration.ConfigurationManager.AppSettings["RabbitMQQueue"];
        private readonly string? _hostname = System.Configuration.ConfigurationManager.AppSettings["RabbitMQHostname"];
        private readonly int _port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RabbitMQPort"]);
        private readonly string? _username = System.Configuration.ConfigurationManager.AppSettings["RabbitMQUsername"];
        private readonly string? _password = System.Configuration.ConfigurationManager.AppSettings["RabbitMQPassword"];
        private readonly string? _vhost = System.Configuration.ConfigurationManager.AppSettings["RabbitMQVHost"];

        private IConnection _connection;
        private IModel _channel;

        public RabbitServices() 
        {
            var factory = new ConnectionFactory { HostName = _hostname, Port = _port, UserName = _username, Password = _password, VirtualHost = _vhost };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

        }

        public void SendMessage<T>(T message)
        {
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "", routingKey: _queue, body: body);
            
        }
    }
}
