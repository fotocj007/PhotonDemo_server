using Common;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler()
        {
            OpCode = OperationCode.Default;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,
            ClientPeer peer)
        {
        }
    }
}