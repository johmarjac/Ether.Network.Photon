using Ether.Network.Common;
using Ether.Network.Photon.Common.Protocol;

namespace Ether.Network.Photon.Common
{
    public abstract class PhotonConnection : NetUser
    {
        public PhotonConnection()
        {
            Protocol = new Protocol16();
        }

        protected ProtocolBase Protocol { get; private set; }
    }
}