using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Thread.Sleep(2000);

            while (true)
            {
                Thread.Sleep(500);

                //位置同步
                SendPoint();
            }
        }

        private void SendPoint()
        {
            var playerDataList = new List<PlayerData>();
            foreach (var peer in MyGameServer.Instance.peerList)
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    var playerD = new PlayerData();
                    playerD.Username = peer.username;
                    playerD.Pos = new Vector3Data {x = peer.x, y = peer.y, z = peer.z};

                    playerDataList.Add(playerD);
                }
            
            if(!playerDataList.Any())
                return;

            var sw = new StringWriter();
            var serializer = new XmlSerializer(typeof(List<PlayerData>));
            serializer.Serialize(sw, playerDataList);
            sw.Close();

            var playerDataString = sw.ToString();

            var data = new Dictionary<byte, object>();
            data.Add((byte) ParameterCode.PlayerDataList, playerDataString);

            foreach (var peer in MyGameServer.Instance.peerList)
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    var ed = new EventData((byte) EventCode.SyncPosition);
                    ed.Parameters = data;
                    peer.SendEvent(ed, new SendParameters());
                }
        }
    }
}