using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeetHut.Backend.Controllers
{
    /// <summary>
    /// Web socket controller
    /// </summary>
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly ILogger<WebSocketController> _logger;
        private readonly HttpContext _context;

        /// <summary>
        /// Init Web socket controller
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="context">HTTP Context</param>
        public WebSocketController(ILogger<WebSocketController> logger, HttpContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get
        /// </summary>
        [HttpGet("ws")]
        public async Task Get()
        {
            if (_context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await _context.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                _context.Response.StatusCode = 400;
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult wsResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!wsResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, wsResult.Count), wsResult.MessageType,
                    wsResult.EndOfMessage, CancellationToken.None);
                wsResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(wsResult.CloseStatus.Value, wsResult.CloseStatusDescription,
                CancellationToken.None);
        }
    }
}