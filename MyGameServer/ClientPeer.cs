using System.Collections.Generic;
using Common;
using Common.Toos;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public string username;
        public float x, y, z;

        //创建客户端
        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
        }

        //响应前端请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            MyGameServer.log.Info("Client---请求了---" + operationRequest.OperationCode);

            //首先获取客户端传来的code (operationRequest.OperationCode)
            //然后根据code 去 MyGameServer中获取注册的Handler。
            //Handler我们注册到了主函数HandlerDict中。
            //DictTool工具类是我们自己定义的,方便传入key,就能从Dict中取值,这里取出的是code相对应的handler
            var handler = DictTool.GetValue(MyGameServer.Instance.HandlerDict,
                (OperationCode) operationRequest.OperationCode);

            if (handler != null)
            {
                //找到相应的Handler,直接调用 OnOperationRequest 进行相应逻辑处理
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                //如果没有找到,返回我们自定义的 DefaultHandler.
                var defHander = DictTool.GetValue(MyGameServer.Instance.HandlerDict,
                    OperationCode.Default);

                defHander.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }

        //处理客户端断开连接
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.log.Info("Client------断开了");
            
            //告诉其他客户端有人离开
            foreach (var tempPeer in MyGameServer.Instance.peerList)
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != this)
                {
                    var ed = new EventData((byte) EventCode.ClosePlayer);
                    var datas = new Dictionary<byte, object>();
                    datas.Add((byte) ParameterCode.UserName, this.username);
                    ed.Parameters = datas;
                    tempPeer.SendEvent(ed, new SendParameters());
                }
            
            MyGameServer.Instance.peerList.Remove(this);
        }
    }
}