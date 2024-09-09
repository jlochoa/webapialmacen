using Microsoft.AspNetCore.SignalR;

namespace WebAPIAlmacen.Hubs
{
    public class MovimientosHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.Others.SendAsync("GetMessage", message);
        }
    }

}
