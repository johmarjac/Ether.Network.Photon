﻿namespace Ether.Network.Photon.Packet
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
    }
}