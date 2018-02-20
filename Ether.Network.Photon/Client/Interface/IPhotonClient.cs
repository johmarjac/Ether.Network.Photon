using Ether.Network.Photon.IO;

namespace Ether.Network.Photon.Client.Interface
{
    public interface IPhotonClient
    {
        void SendOperationRequest();
        void SendEvent();
        void SendPing();
        
        IProtocol SerializationProtocol { get; }
    }
}