using Common;
using Common.Toos;
using MyGameServer.Manager;
using MyGameServer.Model;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            OpCode = OperationCode.Register;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,
            ClientPeer peer)
        {
            //获取前端上传的参数
            var username =
                DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.UserName) as string;
            var password =
                DictTool.GetValue(operationRequest.Parameters, (byte) ParameterCode.Password) as string;

            //返回给前端
            var response = new OperationResponse(operationRequest.OperationCode);

            var userManager = new UserManager();

            //查找是否已经存在该用户名
            var user = userManager.GetByName(username);
            if (user == null)
            {
                //如果不存在,则添加该用户信息
                user = new User {UserName = username, Password = password};
                userManager.Add(user);

                //返回码,成功
                response.ReturnCode = (short) ReturnCode.Success;
            }
            else
            {
                //返回码,失败
                response.ReturnCode = (short) ReturnCode.Failed;
            }

            //给客户端响应
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}