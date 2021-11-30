using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MeetHut.Services.Meet.Hubs
{
    /// <summary>
    /// Message Hub
    /// </summary>
    public class MessageHub : Hub
    {
        /// <summary>
        /// Send new message
        /// </summary>
        /// <param name="message">Message</param>
        public async Task NewMessage(Message message)
        {
            await Clients.All.SendAsync("MessageReceived", message);
        }
    }
}