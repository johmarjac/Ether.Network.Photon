using System;

namespace Ether.Network.Photon.Packet
{
    public class PhotonPacket : PhotonPacketStream
    {
        private PhotonPacket()
        {

        }

        private byte[] BuildBuffer()
        {
            return base.ToArray();
        }

        public override byte[] Buffer => BuildBuffer();

        public bool? IsReliable
        {
            get
            {
                if (Size < 7)
                    return null;

                return Convert.ToBoolean(ToArray()[6]);
            }
        }

        public byte? ChannelId
        {
            get
            {
                if (Size < 6)
                    return null;

                return ToArray()[5];
            }
        }

        public PhotonCode? PhotonPacketType
        {
            get
            {
                if (Size < 9)
                    return null;

                return (PhotonCode)ToArray()[8];
            }
        }

        public enum PhotonCode : byte
        {
            Init,
            InitResponse,
            Operation = 2,
            OperationResponse,
            Event,
            ClientKey = 6,
            ServerKey
        }
    }
}