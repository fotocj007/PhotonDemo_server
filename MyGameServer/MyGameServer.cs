using System.Collections.Generic;
using System.IO;
using Common;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using MyGameServer.Hander;
using MyGameServer.Threads;
using Photon.SocketServer;
using LogManager = ExitGames.Logging.LogManager;

namespace MyGameServer
{
    //继承 ApplicationBase
    public class MyGameServer : ApplicationBase
    {
        //日志打印
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();

        public List<ClientPeer> peerList = new List<ClientPeer>();
        
        //开启一个线程进行位置同步
        private SyncPositionThread syncPosThread = new SyncPositionThread();
        
        public new static MyGameServer Instance { get; private set; }
        
        //客户端创建链接
        //使用一个peerbase表示一个客户端连接
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("Client  Connect----------");
            //创建一个客户端返回给引擎,引擎自动管理
            
            ClientPeer peer = new ClientPeer(initRequest);
            peerList.Add(peer);
            return peer;
        }

        //服务器启动时调用
        protected override void Setup()
        {
            LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "MySer_" + ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(BinaryPath, "log4net.config")));

            log.Info("MyGameServer start----------");


            Instance = this;
            InitHandler();
            syncPosThread.Run();
        }

        private void InitHandler()
        {
            var loginHander = new LoginHander();
            HandlerDict.Add(loginHander.OpCode, loginHander);

            var defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.OpCode, defaultHandler);

            var registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.OpCode, registerHandler);

            var syPosHandler = new SysPositionHandler();
            HandlerDict.Add(syPosHandler.OpCode, syPosHandler);
            
            var syPlayerHandler = new SysPlayerHandler();
            HandlerDict.Add(syPlayerHandler.OpCode, syPlayerHandler);
        }

        //服务器停止调用
        protected override void TearDown()
        {
            log.Info("MyGameServer down----------");
            syncPosThread.Stop();
        }
    }
}