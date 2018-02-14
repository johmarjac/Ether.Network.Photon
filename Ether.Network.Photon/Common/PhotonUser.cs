using Ether.Network.Common;
using Ether.Network.Photon.Common.Interface;
using Ether.Network.Photon.IO;
using Ether.Network.Photon.IO.Protocol;

namespace Ether.Network.Photon.Common
{
    public abstract class PhotonUser : NetUser, IPhotonUser
    {
        public void SendOperationResponse()
        {
            throw new System.NotImplementedException();
        }

        public void SendEvent()
        {
            throw new System.NotImplementedException();
        }

        public virtual IProtocol SerializationProtocol => DefaultProtocol;

        private readonly IProtocol DefaultProtocol = new Protocol16();
    }
}