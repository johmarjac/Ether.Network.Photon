using System;
using Ether.Network.Photon.Server;
using Xunit;

namespace Ether.Network.Photon.Tests.Basic
{
    public sealed class ExampleServer : PhotonServer<ExampleConnection>
    {
        public ExampleServer()
        {
            Configuration.Host = "0.0.0.0";
            Configuration.Port = 4444;
            Configuration.Blocking = false;
            Configuration.BufferSize = 8;
            Configuration.MaximumNumberOfConnections = 5;
            Configuration.Backlog = 100;
        }

        protected override void Initialize()
        {            
        }

        protected override void OnClientConnected(ExampleConnection connection)
        {
        }

        protected override void OnClientDisconnected(ExampleConnection connection)
        {
        }

        protected override void OnError(Exception exception)
        {
            Assert.True(true, $"{exception.ToString()}");
        }
    }
}