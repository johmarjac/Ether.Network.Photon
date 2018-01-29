using Ether.Network.Packets;
using Ether.Network.Photon.Common;
using Ether.Network.Server;

namespace Ether.Network.Photon.Server
{
    public abstract class PhotonServer<T> : NetServer<T> where T : PhotonConnection, new()
    {
        protected override IPacketProcessor PacketProcessor => new PhotonServerPacketProcessor();
    }
}