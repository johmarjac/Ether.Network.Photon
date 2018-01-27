using System;
using System.IO;
using Ether.Network.Client;
using Ether.Network.Packets;
using Ether.Network.Photon.Common;
using Ether.Network.Photon.Common.Protocol;
using Ether.Network.Photon.Packets;

namespace Ether.Network.Photon.Client
{
    public abstract class PhotonClient : NetClient
    {
        protected PhotonClient(string host, int port, int bufferSize) : base(host, port, bufferSize)
        {
            PhotonProtocol = new Protocol16();
        }

        public void SendOperation(OperationRequest request)
        {
            using (var stream = new MemoryStream())
            using (var buffer = new StreamBuffer())
            using (var packet = new PhotonPacket())
            {
                PhotonProtocol.SerializeOperationRequest(buffer, request.OperationCode, request.Parameters, false);
                packet.Write(buffer.GetBuffer());
                Send(packet);
            }
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var packetIdentifier = packet.Read<byte>();
            if (packetIdentifier == 240)
            {
                // Read Ping
                var ping = packet.Read<int>();
                var ping2 = packet.Read<int>();

                // Send Pong
                var data = new byte[] { 240, 0, 0, 0, 0 };
                int num = 1;
                Protocol.Serialize(Environment.TickCount, data, ref num);

                using (var pongPacket = new PhotonPacket(false))
                {
                    pongPacket.Write(data, 0, data.Length);
                    Send(pongPacket);
                }
            }
            else
            {
                var len = packet.Read<int>();
                var channelId = packet.Read<byte>();
                var reliable = packet.Read<byte>();
                var bla = packet.Read<byte>(); // wtf indicates this? that its a TCP packet?
                var bla2 = packet.Read<byte>();
                var msgType = (byte)(bla2 & 127);
                var encrypted = (byte)(bla2 & 128);

                switch (msgType)
                {

                }
            }
        }

        protected override IPacketProcessor PacketProcessor => new PhotonPacketProcessor();
        protected ProtocolBase PhotonProtocol { get; private set; }
    }
}