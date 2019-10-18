using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public class SysPlayerHandler : BaseHandler
    {
        public SysPlayerHandler()
        {
            OpCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,
            ClientPeer peer)
        {
            //取得所有玩家信息
            var usernameLists = new List<string>();
            foreach (var tempPeer in MyGameServer.Instance.peerList)
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer)
                    usernameLists.Add(tempPeer.username);

            //序列化为一个字符串
            var sw = new StringWriter();
            var serializer = new XmlSerializer(typeof(List<string>));
            serializer.Serialize(sw, usernameLists);
            sw.Close();
            var userString = sw.ToString();
            
            //将所有用户发送给新加入的客户端
            var data = new Dictionary<byte, object>();
            data.Add((byte) ParameterCode.UsernameList, userString);
            var response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;
            peer.SendOperationResponse(response, sendParameters);

            //告诉其他客户端有新用户加入
            foreach (var tempPeer in MyGameServer.Instance.peerList)
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer)
                {
                    var ed = new EventData((byte) EventCode.NewPlayer);
                    var datas = new Dictionary<byte, object>();
                    datas.Add((byte) ParameterCode.UserName, peer.username);
                    ed.Parameters = datas;
                    tempPeer.SendEvent(ed, sendParameters);
                }
        }
    }
}