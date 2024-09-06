using Microsoft.AspNetCore.SignalR;
using SignalRChat.Classes;
using static System.Net.Mime.MediaTypeNames;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub<IChat>
    {
        public static List<Conexion> conexiones = new List<Conexion>();
        // Clients son los clientes conectados al Hub. En Clients hay métodos para manejar usuarios
        // GetMessage es un evento que debe estar registrado en el cliente
        // Los argumentos son la información que va a ir al cliente. Pueden ir objetos 
        public async Task SendMessage(Message message)
        {
            if (!string.IsNullOrEmpty(message.Text))
            {
                //Código para enviar a todos los usuarios de todas las salas
                //await Clients.All.GetMessage(message);
                await Clients.Group(message.Room).GetMessage(message);
            }
            else if (!string.IsNullOrEmpty(message.User))
            {
                conexiones.Add(new Conexion
                {
                    Id = Context.ConnectionId,
                    User = message.User,
                    Avatar = message.Avatar,
                    Room = message.Room
                });

                //Código para enviar a todos los usuarios de todas las salas
                //await Clients.AllExcept(Context.ConnectionId).GetMessage(new Message()
                //{
                //    User = message.User,
                //    Avatar = message.Avatar,
                //    Text = " se ha conectado!"
                //});

                // Asignar sala al usuario
                await Groups.AddToGroupAsync(Context.ConnectionId, message.Room);
                await Clients.GroupExcept(message.Room, Context.ConnectionId).GetMessage(new Message()
                {
                    User = message.User,
                    Avatar = message.Avatar,
                    Text = " se ha conectado!"
                });
            }
        }

        // Sobrescribimos (override) algunos métodos para añadirle algo más de lógica
        public override async Task OnConnectedAsync()
        {
            // Cuando un usuario se conecta, se le da la bienvenida solo a ese por su id
            await Clients.Client(Context.ConnectionId).GetMessage(new Message() { User = "Host", Text = "Hola, Bienvenido al Chat", Avatar = "https://st2.depositphotos.com/40273566/43738/v/380/depositphotos_437386202-stock-illustration-logistic-logo-design-vector-illustration.jpg" });

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var conexion = conexiones.Where(x => x.Id == Context.ConnectionId).FirstOrDefault();
            //Código para enviar a todos los usuarios de todas las salas
            //await Clients.AllExcept(Context.ConnectionId).GetMessage(new Message() { User = "Host", Text = $"{conexion.User} ha salido del chat", Avatar = "https://st2.depositphotos.com/40273566/43738/v/380/depositphotos_437386202-stock-illustration-logistic-logo-design-vector-illustration.jpg" });
            await Clients.GroupExcept(conexion.Room, Context.ConnectionId).GetMessage(
                new Message()
                {
                    User = "Host",
                    Text = $"{conexion.User} ha salido del chat",
                    Avatar = "https://st2.depositphotos.com/40273566/43738/v/380/depositphotos_437386202-stock-illustration-logistic-logo-design-vector-illustration.jpg"
                });

            conexiones.Remove(conexion);

            await base.OnDisconnectedAsync(exception);
        }
    }


}
