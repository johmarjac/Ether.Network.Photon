using Ether.Network.Photon.Tests.Basic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ether.Network.Photon.Tests
{
    public class BasicTests
    {
        [Fact]
        public async void ServerClientConnectionInitialization()
        {
            using (var server = new ExampleServer())
            using (var client = new ExampleClient())
            {
                server.Start();
                await Task.Delay(500);

                Assert.True(server.IsRunning);

                client.Connect();
                await Task.Delay(500);

                Assert.True(client.IsConnected);

                while(true)
                { Thread.Sleep(1); }
            }
        }
    }
}