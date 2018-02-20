using Ether.Network.Client;
using Ether.Network.Packets;
using Ether.Network.Photon.Client.Interface;
using Ether.Network.Photon.IO;
using Ether.Network.Photon.IO.Protocol;
using Ether.Network.Photon.Packet;
using System;

namespace Ether.Network.Photon.Client
{
    public abstract class PhotonClient : NetClient, IPhotonClient
    {
        public void SendOperationRequest()
        {
            throw new NotImplementedException();
        }

        public void SendEvent()
        {
            throw new NotImplementedException();
        }

        public void SendPing()
        {
            using (var packet = PhotonPacket.PingTemplate())
            {
                var buf = new byte[4];
                var offs = 0;
                Protocol.Serialize(Environment.TickCount, buf, ref offs);

                Send(packet);
            }
        }

        protected override void OnConnected()
        {
            Send(PhotonPacket.InitTemplate());
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var photonPacket = new PhotonPacket(packet.Buffer);

            if (photonPacket.IsPingPacket.HasValue && photonPacket.IsPingPacket.Value)
                SendPing();
            else
            {
                if (photonPacket.PhotonPacketType.HasValue)
                {
                    switch (photonPacket.PhotonPacketType.Value)
                    {
                        case PhotonPacket.PhotonCode.InitResponse:

                            break;
                        default:
                            throw new InvalidOperationException("Unhandled PhotonCode!");
                    }
                }
                else
                    throw new InvalidOperationException("Invalid Packet!");
            }
        }

        protected override IPacketProcessor PacketProcessor => new PhotonPacketProcessor();

        public virtual IProtocol SerializationProtocol => DefaultProtocol;

        private readonly IProtocol DefaultProtocol = new Protocol16();
    }
}