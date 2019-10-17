using Common;
using Common.Toos;
using MyGameServer.Manager;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public class LoginHander : BaseHandler
    {
        public LoginHander()
        {
            OpCode = OperationCode.Login;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,
            ClientPeer peer)
        {
            //利用工具类从获取客户端上传的参数
            var username =
                DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.UserName) as string;
            var password =
                DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Password) as string;

            //数据库管理类
            var userManager = new UserManager();

            //检测用户名和密码是否正确
            var isOk = userManager.VerifyModel(username, password);

            //返回给客户端数据
            var response = new OperationResponse(operationRequest.OperationCode);
            if (isOk)
            {
                response.ReturnCode = (short) ReturnCode.Success;

                peer.username = username;
            }
            else
            {
                response.ReturnCode = (short) ReturnCode.Failed;
            }

           
            //给客户端响应
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}