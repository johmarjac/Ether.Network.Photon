using Ether.Network.Packets;

namespace Ether.Network.Photon.Common
{
    public class PhotonPacket : NetPacketStream
    {
        public PhotonPacket()
        {
        }

        public PhotonPacket(byte[] buffer) : base(buffer)
        {
        }
    }
}