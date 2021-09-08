using System.Text.Json;

namespace MeetHut.Backend.Middlewares
{
    /// <summary>
    /// Server exception
    /// </summary>
    public class ServerException
    {
        /// <summary>
        /// Status code 
        /// </summary>
        public int StatusCode { get; set; }
        
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}