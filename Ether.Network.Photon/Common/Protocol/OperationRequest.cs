using System.Collections.Generic;

namespace Ether.Network.Photon.Common.Protocol
{
    public class OperationRequest
    {
        public OperationRequest()
        {
        }

        public byte OperationCode;

        public Dictionary<byte, object> Parameters;
    }
}