using Ether.Network.Photon.IO;

namespace Ether.Network.Photon.Common.Interface
{
    public interface IPhotonUser
    {
        void SendOperationResponse();
        void SendEvent();

        IProtocol SerializationProtocol { get; }
    }
}