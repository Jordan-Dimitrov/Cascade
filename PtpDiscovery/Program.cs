using CascadeAPI;

namespace PtpDiscovery
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DiscoveryServer discoveryServer = new DiscoveryServer();
            await discoveryServer.StartAsync();
        }
    }
}
