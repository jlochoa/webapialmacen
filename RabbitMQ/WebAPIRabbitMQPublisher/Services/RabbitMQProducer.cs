using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using WebAPIRabbitMQPublisher.DTOs;

namespace WebAPIRabbitMQPublisher.Services
{
    public class RabbitMQProducer
    {
        public void SendMessage(DTOPedido message)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5672;
            factory.UserName = "guest";
            factory.Password = "guest";

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // Exchange (pimer receptor) del message (en nuestro caso el pedido)
            // No hace falta crearlo en Rabbit, se crea de forma automátia
            channel.ExchangeDeclare("ex.pedidos", "direct", true, false);
            // Queue (cola) a la que enviará los messages (pedidos) el exchange
            // ex.pedidos. Esta cola sí hay que crearla en Rabbit
            channel.QueueDeclare("pedidos", true, false, false, null);
            // Vinculamos el exchange ex.pedidos con la queue pedidos
            // También especificamos la ruta donde dirigir los messages (pedidos)
            channel.QueueBind("pedidos", "ex.pedidos", "pedidos");

            // Codificación interna para que el message viaje por el canal
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            // publicamos (enviamos) el mensaje a Rabbit
            channel.BasicPublish(exchange: "ex.pedidos", routingKey: "pedidos", body: body);
        }
    }

}
