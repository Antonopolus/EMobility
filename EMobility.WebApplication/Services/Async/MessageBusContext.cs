using EMobility.Data;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EMobility.WebApi.Services.Async
{
    public class MessageBusContext : IMessageBusClient
    {
        private readonly IConfiguration Configuration;
        private IConnection connection;
        public IModel channel;

        public MessageBusContext(IConfiguration configuration)
        {
            Configuration = configuration;
            var HostName = Configuration["RabbitMQHost"];
            var Port = Int32.Parse(Configuration["RabbitMQPort"]);


            var factory = new ConnectionFactory()
            {
                HostName = Configuration["RabbitMQHost"],
                Port = Int32.Parse(Configuration["RabbitMQPort"])
            };

            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                connection.ConnectionShutdown += Connection_ConnectionShutdown;

            }
            catch (Exception ex) { }//
            //Log.Error(ex, "MessageBus ERROR"); }

        }

        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {

        }

        public void PublishNewChargingPoint(ChargingPoint chargingPoint)
        {
            var message = JsonSerializer.Serialize(chargingPoint);
            if (connection.IsOpen)
            {
                SendMessage(message);
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body
                );
        }

        public void Dispose()
        {
            if (channel.IsOpen) { }
            channel.Close();
            connection.Close();
        }

    }
}
