using BackgroundWorkerService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text.Json;

using BackgroundWorkerService.Models;

namespace BackgroundWorkerService.Utils
{
    public class RabbitServices : IRabbitServices
    {
        private ConnectionFactory? _connectionFactory;
        private IConnection? _connection;
        private IModel? _channel;

        private readonly string? _queue = System.Configuration.ConfigurationManager.AppSettings["RabbitMQQueue"];
        private readonly string? _hostname = System.Configuration.ConfigurationManager.AppSettings["RabbitMQHostname"];
        private readonly int _port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RabbitMQPort"]);
        private readonly string? _username = System.Configuration.ConfigurationManager.AppSettings["RabbitMQUsername"];
        private readonly string? _password = System.Configuration.ConfigurationManager.AppSettings["RabbitMQPassword"];
        private readonly string? _vhost = System.Configuration.ConfigurationManager.AppSettings["RabbitMQVHost"];

        private int _workerCount = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["WorkerCount"]);

        private static IDBServices _dBServices;

        public RabbitServices(IDBServices dBServices)
        {
            _connectionFactory = new ConnectionFactory { HostName = _hostname, Port = _port, UserName = _username, Password = _password, VirtualHost = _vhost };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            _dBServices = dBServices;
        }

        public void Start()
        {

            Console.WriteLine("Creating workers");

            for (int i = 0; i < _workerCount; i++)
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    byte[] body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var time = DateTime.Now;
                    Console.WriteLine($"{time}: Received {message}");

                    //convert message to list of hashitems
                    var hashItems = JsonSerializer.Deserialize<List<HashItem>>(message);

                    //save message to database
                    _dBServices.SaveHashesInDB(hashItems);


                    Thread.Sleep(200);


                    // here channel could also be accessed as ((EventingBasicConsumer)sender).Model
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                _channel.BasicConsume(queue: _queue,
                     autoAck: false,
                     consumer: consumer);

                Console.WriteLine("Worker created");
            }


        }
    }
}
