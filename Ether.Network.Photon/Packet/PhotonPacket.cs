using System;

namespace Ether.Network.Photon.Packet
{
    public class PhotonPacket : PhotonPacketStream
    {
        public PhotonPacket()
        {
        }

        public PhotonPacket(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
        }

        private byte[] BuildBuffer()
        {
            return base.ToArray();
        }

        public override byte[] Buffer => BuildBuffer();

        public bool? IsPingPacket
        {
            get
            {
                if (Size < 1)
                    return null;

                return Convert.ToBoolean(ToArray()[0] == 0xF0);
            }
        }

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

        public static PhotonPacket OperationRequestTemplate(byte channelId = 0, bool reliable = true, bool encrypted = false)
        {
            var packet = new PhotonPacket();
            packet.Write((byte)0xFB);
            packet.Write(0);
            packet.Write(channelId);
            packet.Write(Convert.ToByte(reliable));
            packet.Write((byte)0xF3);

            if (encrypted)
                packet.Write((((byte)PhotonCode.Operation)) | 128);
            else
                packet.Write((byte)PhotonCode.Operation);

            return packet;
        }

        public static PhotonPacket OperationResponseTemplate(byte channelId = 0, bool reliable = true, bool encrypted = false)
        {
            var packet = new PhotonPacket();
            packet.Write((byte)0xFB);
            packet.Write(0);
            packet.Write(channelId);
            packet.Write(Convert.ToByte(reliable));
            packet.Write((byte)0xF3);

            if (encrypted)
                packet.Write((((byte)PhotonCode.OperationResponse)) | 128);
            else
                packet.Write((byte)PhotonCode.OperationResponse);

            return packet;
        }

        public enum PhotonCode : byte
        {
            Init,
            InitResponse,
            Operation,
            OperationResponse,
            Event,
            ClientKey = 6,
            ServerKey
        }
    }
}