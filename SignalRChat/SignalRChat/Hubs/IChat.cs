using SignalRChat.Classes;

namespace SignalRChat.Hubs
{
    public interface IChat
    {
        Task SendMessage(Message message);
        Task GetMessage(Message message);
    }
}
