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
        /// <param name="publicId">Public Id</param>
        /// <param name="message">Message</param>
        public async Task NewMessage(string publicId, Message message)
        {
            await Clients.Groups(publicId).SendCoreAsync("MessageReceived", new object[] { message });
        }

        /// <summary>
        /// Connect to specified group by Room public Id
        /// </summary>
        /// <param name="publicId">Public Id</param>
        public async Task AddToGroup(string publicId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, publicId);
        }

        /// <summary>
        /// Disconnect from a specified group by Room public Id
        /// </summary>
        /// <param name="publicId">Public Id</param>
        public async Task RemoveFromGroup(string publicId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, publicId);
        }
    }
}