using Common;
using Common.Toos;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public class SysPositionHandler : BaseHandler
    {
        public SysPositionHandler()
        {
            OpCode = OperationCode.SyncPosition;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,
            ClientPeer peer)
        {
//            Vector3Data pos =
//                (Vector3Data) DictTool.GetValue<byte, Object>(operationRequest.Parameters,
//                    (byte) ParameterCode.Position);

            var x =
                (float) DictTool.GetValue(operationRequest.Parameters,
                    (byte) ParameterCode.X);
            var y =
                (float) DictTool.GetValue(operationRequest.Parameters,
                    (byte) ParameterCode.Y);
            var z =
                (float) DictTool.GetValue(operationRequest.Parameters,
                    (byte) ParameterCode.Z);
            
            peer.x = x;
            peer.y = y;
            peer.z = z;
            
        }
    }
}