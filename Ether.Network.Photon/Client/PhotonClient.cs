using Ether.Network.Client;
using Ether.Network.Packets;
using Ether.Network.Photon.Client.Interface;
using Ether.Network.Photon.IO;
using Ether.Network.Photon.IO.Protocol;
using System;

namespace Ether.Network.Photon.Client
{
    public abstract class PhotonClient : NetClient, IPhotonClient
    {
        protected PhotonClient(string host, int port, int bufferSize) : base(host, port, bufferSize)
        {
        }

        public void SendOperationRequest()
        {
            throw new NotImplementedException();
        }

        public void SendEvent()
        {
            throw new NotImplementedException();
        }

        protected override IPacketProcessor PacketProcessor => throw new NotImplementedException();

        public virtual IProtocol SerializationProtocol => DefaultProtocol;

        private readonly IProtocol DefaultProtocol = new Protocol16();
    }
}