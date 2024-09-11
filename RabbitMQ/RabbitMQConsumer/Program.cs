using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

IConnection connection;
IModel channel;

// Creamos conexión
ConnectionFactory factory = new ConnectionFactory();
factory.HostName = "localhost";
factory.VirtualHost = "/";
factory.Port = 5672;
factory.UserName = "guest";
factory.Password = "guest";


connection = factory.CreateConnection();
channel = connection.CreateModel();
// Creamos canal
var consumer = new EventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume("pedidos", true, consumer);

Console.WriteLine("Esperando pedidos...");
Console.ReadKey();

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    string pedido = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine("Pedido: " + pedido);
}
