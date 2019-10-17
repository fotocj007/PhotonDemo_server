using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Toos;
using Photon.SocketServer;

namespace MyGameServer.Hander
{
    public class SysPlayerHandler : BaseHandler
    {
        public SysPlayerHandler()
        {
            OpCode = OperationCode.SyncPlayer;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters,ClientPeer peer)
        {
            //取得所有玩家信息
            List<string> usernameLists = new List<string>();
            foreach (ClientPeer tempPeer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer)
                {
                    usernameLists.Add(tempPeer.username);
                }
            }
            
            usernameLists.Add("2333");
            usernameLists.Add("45ttt");
            
            //序列化为一个字符串
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            serializer.Serialize(sw, usernameLists);
            sw.Close();
            string userString = sw.ToString();
            

            //直接传递字符串
            Dictionary<byte,object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.UsernameList, userString);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.Parameters = data;
            peer.SendOperationResponse(response, sendParameters);
            
            //告诉其他客户端
            foreach (ClientPeer tempPeer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(tempPeer.username) == false && tempPeer != peer)
                {
                    EventData ed = new EventData((byte)EventCode.NewPlayer);
                    Dictionary<byte,object> datas = new Dictionary<byte, object>();
                    datas.Add((byte) ParameterCode.UserName, peer.username);
                    ed.Parameters = datas;
                    tempPeer.SendEvent(ed,sendParameters);
                }
            }
        }
    }
}