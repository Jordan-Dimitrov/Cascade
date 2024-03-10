using System.Net.Sockets;
using System.Net;

namespace CascadeAPI
{
    public class DiscoveryServer
    {
        private Dictionary<string, List<string>> _SongLibrary;
        private TcpListener _Listener;

        public DiscoveryServer()
        {
            _SongLibrary = new Dictionary<string, List<string>>();
            _Listener = new TcpListener(IPAddress.Any, 8080);
        }

        public async Task StartAsync()
        {
            _Listener.Start();
            Console.WriteLine("Discovery Server Started.");

            while (true)
            {
                TcpClient client = await _Listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }
        private async Task HandleClientAsync(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string requestData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

            string[] requestParts = requestData.Split('|');
            string command = requestParts[0];

            string clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            string port = ((IPEndPoint)client.Client.RemoteEndPoint).Port.ToString();

            string address = clientAddress + ":" + port;

            if (command == "ADDSONGS")
            {
                string[] songs = requestParts[1].Split(',');
                foreach (string song in songs)
                {
                    if (!_SongLibrary.ContainsKey(song))
                    {
                        _SongLibrary[song] = new List<string>();
                    }

                    if (!_SongLibrary[song].Contains(address))
                    {
                        _SongLibrary[song].Add(address);
                    }
                }
            }
            else if (command == "REQUESTSONG")
            {
                string songName = requestParts[1];

                if (_SongLibrary.ContainsKey(songName))
                {
                    string providerAddress = _SongLibrary[songName][0];

                    _SongLibrary[songName].RemoveAt(0);
                    _SongLibrary[songName].Add(providerAddress);

                    byte[] responseBuffer = System.Text.Encoding.ASCII.GetBytes(providerAddress);
                    await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                }
                else
                {
                    byte[] responseBuffer = System.Text.Encoding.ASCII.GetBytes("Song not found");
                    await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                }
            }

            client.Close();
        }
    }
}
