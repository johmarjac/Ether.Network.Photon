using System;
using System.Net.Sockets;
using Ether.Network.Packets;
using Ether.Network.Photon.Client;
using Xunit;

namespace Ether.Network.Photon.Tests.Basic
{
    public sealed class ExampleClient : PhotonClient
    {
        public ExampleClient()
        {
            Configuration.Host = "127.0.0.1";
            Configuration.Port = 4444;
            Configuration.BufferSize = 8;
        }

        protected override void OnConnected()
        {
            base.OnConnected();
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);


        }

        protected override void OnDisconnected()
        {
        }

        protected override void OnSocketError(SocketError socketError)
        {
            Assert.True(true, Enum.GetName(typeof(SocketError), socketError));
        }
    }
}