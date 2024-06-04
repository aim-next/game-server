using game_server.Network;

var server = new WebSocketServer(8080); // Replace with your desired port
await server.Start();
