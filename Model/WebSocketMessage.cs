using System.Net.WebSockets;

namespace game_server.Model
{
    public class WebSocketMessage
    {
        public WebSocketMessageType MessageType { get; set; }
        public string Payload { get; set; } = null!;
    }
}