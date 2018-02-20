using Ether.Network.Packets;
using Ether.Network.Photon.IO.Protocol;
using Ether.Network.Photon.Packet;

namespace Ether.Network.Photon.Server
{
    public sealed class PhotonPacketProcessor : IPacketProcessor
    {
        public int HeaderSize => 5;

        public bool IncludeHeader => true;

        public int GetLength(byte[] buffer)
        {
            if (buffer[0] == 240)
                return HeaderSize;

            var offs = 1;
            var len = 0;
            Protocol.Deserialize(out len, buffer, ref offs);

            return len;
        }

        public INetPacketStream CreatePacket(byte[] buffer)
        {
            return new PhotonPacket(buffer);
        }
    }
}