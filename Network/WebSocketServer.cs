using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using game_server.Model;

namespace game_server.Network
{
    public class WebSocketServer
    {
        private readonly int port;

        public WebSocketServer(int port)
        {
            this.port = port;
        }

        public async Task Start()
        {
            var listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Console.WriteLine($"WebSocket server listening on port {port}");

            while (true)
            {
                var tcpClient = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected");

                await HandleWebSocketConnection(tcpClient);
            }
        }

        private async Task HandleWebSocketConnection(TcpClient tcpClient)
        {
            using (var networkStream = tcpClient.GetStream())
            {
                // Perform WebSocket handshake (simplified for brevity)
                var handshakeData = await ReadHandshakeRequest(networkStream);
                ValidateHandshake(handshakeData); // Implement validation logic

                await WriteHandshakeResponse(networkStream);

                // Start processing messages after handshake
                await ProcessWebSocketMessages(networkStream);
            }
        }

        private async Task<string> ReadHandshakeRequest(NetworkStream stream)
        {
            var buffer = new byte[1024];
            int bytesRead = 0;
            StringBuilder requestData = new StringBuilder();

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                requestData.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                // Check for end of handshake request (implementation omitted for brevity)
            }

            return requestData.ToString();
        }

        private void ValidateHandshake(string handshakeData)
        {
            // Implement handshake data validation logic
            // (Ensure presence of required headers, etc.)
        }

        private async Task WriteHandshakeResponse(NetworkStream stream)
        {
            // Implement logic to generate a valid WebSocket handshake response
            // based on the received request data
            string response = "HTTP/1.1 101 Switching Protocols\r\n" +
                               "Upgrade: websocket\r\n" +
                               "Connection: Upgrade\r\n" +
                               // Add other required headers
                               "\r\n";
            await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
        }

        private async Task ProcessWebSocketMessages(NetworkStream stream)
        {
            while (true)
            {
                var message = await ReadWebSocketMessage(stream);
                if (message.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                // Process the received message data (decode payload based on opcode)
                Console.WriteLine($"Received message: {message.Payload}");

                // Optionally, send a response message
                await SendTextMessage(stream, $"Echo: {message.Payload}");
            }
        }

        private async Task<WebSocketMessage> ReadWebSocketMessage(NetworkStream stream)
        {
            // Implement logic to read a WebSocket message frame from the stream
            // considering opcode, payload length, masking, etc. (refer to RFC 6455)

            // This example omits the implementation for brevity
            return new WebSocketMessage { MessageType = WebSocketMessageType.Text, Payload = "" };
        }

        private async Task SendTextMessage(NetworkStream stream, string message)
        {
            // Implement logic to send a text message frame according to the WebSocket protocol
            // (consider fragmentation for large messages)

            // This example omits the implementation for brevity
        }
    }
}