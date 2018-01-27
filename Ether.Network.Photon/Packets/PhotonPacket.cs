using Ether.Network.Packets;
using Ether.Network.Photon.Common.Protocol;

namespace Ether.Network.Photon.Packets
{
    public class PhotonPacket : NetPacketStream
    {
        public PhotonPacket(bool finalizeBuffer = true)
        {
            FinalizeBuffer = finalizeBuffer;
        }

        private byte[] GetFinalizedBuffer()
        {
            if(FinalizeBuffer)
            {
                var len = new byte[4];
                long oldPosition = Position;
                var offset = 0;
                Protocol.Serialize(Size, len, ref offset);
                Seek(1, System.IO.SeekOrigin.Begin);
                Write(len, 0, len.Length);
                Seek(oldPosition, System.IO.SeekOrigin.Begin);
            }
            return base.Buffer;
        }

        public override byte[] Buffer => GetFinalizedBuffer();
        protected bool FinalizeBuffer { get; private set; }
    }
}