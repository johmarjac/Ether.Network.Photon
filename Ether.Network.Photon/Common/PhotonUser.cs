using Ether.Network.Common;
using Ether.Network.Packets;
using Ether.Network.Photon.Common.Interface;
using Ether.Network.Photon.IO;
using Ether.Network.Photon.IO.Protocol;
using Ether.Network.Photon.Packet;
using System;

namespace Ether.Network.Photon.Common
{
    public abstract class PhotonUser : NetUser, IPhotonUser
    {
        public void SendOperationResponse()
        {
            throw new System.NotImplementedException();
        }

        public void SendEvent()
        {
            throw new System.NotImplementedException();
        }

        public void SendPing(int clientTime)
        {
            using (var packet = PhotonPacket.PingTemplate())
            {
                var buf = new byte[4];
                var offs = 0;
                Protocol.Serialize(Environment.TickCount, buf, ref offs);
                packet.Write(clientTime);

                Send(packet);
            }
        }

        public override void HandleMessage(INetPacketStream packet)
        {
            base.HandleMessage(packet);

            var photonPacket = new PhotonPacket(packet.Buffer);

            if (photonPacket.IsPingPacket.HasValue && photonPacket.IsPingPacket.Value)
                SendPing(packet.Read<int>());
            else
            {
                if (photonPacket.PhotonPacketType.HasValue)
                {
                    switch (photonPacket.PhotonPacketType.Value)
                    {
                        case PhotonPacket.PhotonCode.Init:
                            Send(PhotonPacket.InitResponseTemplate());
                            break;
                        default:
                            throw new InvalidOperationException("Unhandled PhotonCode!");
                    }
                }
                else
                    throw new InvalidOperationException("Invalid Packet!");
            }
        }

        protected abstract void HandleOperationRequest(OperationRequest request);

        public virtual IProtocol SerializationProtocol => DefaultProtocol;

        private readonly IProtocol DefaultProtocol = new Protocol16();
    }
}