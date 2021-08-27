using Heritage.Services.Interfaces.Contracts;
using Heritage.Services.Interfaces.Helpers.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Text;

namespace Heritage.Infrastructure.Business.Support.RabbitMQ
{
    public class RabbitMQService : IEventBus
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _password;
        private readonly string _username;
        private readonly int _port;
        private IConnection _connection;

        public RabbitMQService(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;

            CreateConnection();
        }

        public void Publish(object message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var serializer = new JsonSerializer();
                using var writer = new StringWriter();
                serializer.Serialize(writer, message);
                string serializedMessage = writer.ToString();

                var body = Encoding.UTF8.GetBytes(serializedMessage);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password,
                    Port = _port
                };
                if (ConnectionExists() == false)
                    _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            //CreateConnection();

            return /*_connection != null*/false;
        }
    }
}
