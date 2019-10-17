using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using Common;
using Photon.SocketServer;

namespace MyGameServer.Threads
{
    public class SyncPositionThread
    {
        private Thread ts;

        public void Run()
        {
            ts = new Thread(UpdatePos);

            ts.IsBackground = true;
            ts.Start();
        }

        public void Stop()
        {
            ts.Abort();
        }

        private void UpdatePos()
        {
            Thread.Sleep(5000);

            while (true)
            {
                Thread.Sleep(200);
                
                //位置同步
                SendPoint();
            }
        }

        private void SendPoint()
        {
            List<PlayerData> playerDataList = new List<PlayerData>();
            foreach (ClientPeer peer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    PlayerData playerD = new PlayerData();
                    playerD.Username = peer.username;
                    playerD.Pos = new Vector3Data(){x=peer.x,y=peer.y,z=peer.z};
                    
                    playerDataList.Add(playerD);
                }
            }
            
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            serializer.Serialize(sw,playerDataList);
            sw.Close();

            string playerDataString = sw.ToString();
            
            Dictionary<byte,object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerDataList,playerDataString);
            
            foreach (ClientPeer peer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                   EventData ed = new EventData((byte)EventCode.SyncPosition);
                   ed.Parameters = data;
                   peer.SendEvent(ed,new SendParameters());
                }
            }
            
        }
        
    }
}