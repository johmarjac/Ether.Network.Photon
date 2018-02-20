using Ether.Network.Photon.IO.Protocol;
using Ether.Network.Photon.Util;
using System;

namespace Ether.Network.Photon.Packet
{
    public sealed class PhotonPacket : PhotonPacketStream
    {
        public PhotonPacket()
        {
        }

        public PhotonPacket(byte[] buffer)
        {
            Write(buffer, 0, buffer.Length);
            Seek(0, System.IO.SeekOrigin.Begin);
        }

        private byte[] BuildBuffer()
        {
            if (IsPingPacket == null || (IsPingPacket.HasValue && IsPingPacket.Value))
                return ToArray();

            var oldPosition = Position;
            Seek(1, System.IO.SeekOrigin.Begin);
            SerializeBigEndianInt32(Size);
            Seek(oldPosition, System.IO.SeekOrigin.Begin);

            return ToArray();
        }

        private void SerializeBigEndianInt32(int value)
        {
            var offs = 0;
            var bLen = new byte[4];
            Protocol.Serialize(value, bLen, ref offs);
            Write(bLen, 0, bLen.Length);
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

        public static PhotonPacket PingTemplate()
        {
            var packet = new PhotonPacket();
            packet.Write((byte)0xF0);
            return packet;
        }

        public static PhotonPacket InitTemplate()
        {
            var packet = new PhotonPacket();
            var initBytes = "fb000000300001f30001061e410106004c6f616442616c616e63696e6700000000000000000000000000000000000000".ToArray();
            packet.Write(initBytes, 0, initBytes.Length);
            return packet;
        }

        public static PhotonPacket InitResponseTemplate()
        {
            var packet = new PhotonPacket();
            var initBytes = "fb0000000a0001f30100".ToArray();
            packet.Write(initBytes, 0, initBytes.Length);
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