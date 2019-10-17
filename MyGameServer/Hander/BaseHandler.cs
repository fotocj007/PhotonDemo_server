using Common;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public abstract class BaseHandler
    {
        public OperationCode OpCode;

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,
            ClientPeer peer);
    }
}