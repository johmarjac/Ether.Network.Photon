using Ether.Network.Packets;
using Ether.Network.Photon.Common;
using Ether.Network.Photon.Packets;
using Ether.Network.Server;

namespace Ether.Network.Photon.Server
{
    public abstract class PhotonServer : NetServer<PhotonConnection>
    {
        protected override IPacketProcessor PacketProcessor => new PhotonPacketProcessor();
    }
}