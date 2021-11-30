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
        public String ClientId { get; set; }
        
        /// <summary>
        /// Sender user
        /// </summary>
        public String SenderId { get; set; }
        
        /// <summary>
        /// Message content
        /// </summary>
        public String Text { get; set; }
        
        /// <summary>
        /// Sending time
        /// </summary>
        public DateTime SendingTime { get; set; }
    }
}