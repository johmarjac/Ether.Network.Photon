using Ether.Network.Packets;
using Ether.Network.Photon.Common;
using Ether.Network.Photon.Common.Protocol;

namespace Ether.Network.Photon.Client
{
    internal class PhotonClientPacketProcessor : IPacketProcessor
    {
        public int HeaderSize => 9;

        public bool IncludeHeader => true;

        public int GetLength(byte[] buffer)
        {
            if (buffer[0] == 240)
                return HeaderSize;

            var len = 0;
            var offs = 1;
            Protocol.Deserialize(out len, buffer, ref offs);

            return len;
        }

        public INetPacketStream CreatePacket(byte[] buffer)
        {
            return new PhotonPacket(buffer);
        }
    }
}