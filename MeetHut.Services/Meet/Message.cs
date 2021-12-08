using System;

namespace MeetHut.Services.Meet
{
    /// <summary>
    /// Chat message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Angular Client unique Id
        /// </summary>
        public string ClientId { get; set; }
        
        /// <summary>
        /// Sender user
        /// </summary>
        public int SenderId { get; set; }
        
        /// <summary>
        /// Sender name
        /// </summary>
        public string SenderName { get; set; }
        
        /// <summary>
        /// Message content
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Sending time
        /// </summary>
        public DateTime SendingTime { get; set; }
    }
}