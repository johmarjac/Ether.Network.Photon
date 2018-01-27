using Ether.Network.Packets;
using Ether.Network.Photon.Common.Protocol;

namespace Ether.Network.Photon.Packets
{
    internal sealed class PhotonPacketProcessor : IPacketProcessor
    {
        public int GetLength(byte[] buffer)
        {
            if (buffer[0] == 240)
                return HeaderSize;

            int offset = 1;
            var len = 0;
            Protocol.Deserialize(out len, buffer, ref offset);
            return len;
        }

        public INetPacketStream CreatePacket(byte[] buffer)
        {
            return new NetPacket(buffer);
        }

        public int HeaderSize => 5;
        public bool IncludeHeader => true;
    }
}