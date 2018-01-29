using Ether.Network.Client;
using Ether.Network.Packets;

namespace Ether.Network.Photon.Client
{
    public abstract class PhotonClient : NetClient
    {
        protected PhotonClient(string host, int port, int bufferSize) : base(host, port, bufferSize)
        {
        }

        protected override IPacketProcessor PacketProcessor => new PhotonClientPacketProcessor();
    }
}