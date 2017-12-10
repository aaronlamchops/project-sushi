using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Messages;
using SharedObjects;
using CommSubSystem;
using CommSubSystem.ConversationClass;
using CommSubSystem.Conversations;
using log4net;
using System.Net;
using System.Net.Sockets;

namespace UserApp
{
    public class ClientReceive : Receiver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClientReceive));
        private IPEndPoint gameServer;
        protected List<TCPClient> tcpClients = new List<TCPClient>();

        protected override void ExecuteBasedOnType(byte[] bytes, TypeOfMessage type, IPEndPoint refEp)
        {
            switch (type)
            {
                case TypeOfMessage.LobbyHeartbeat:
                    LobbyHeartBeatResponse(bytes, refEp);
                    break;
                case TypeOfMessage.ConnectInfoMsg:
                    ConnectInfoResponse(bytes, refEp);
                    break;
                default:
                    EnqueueMessage(bytes);
                    break;
            }
        }

        private void EnqueueMessage(byte[] bytes)
        {
            Message msg = Message.Decode<Message>(bytes);
            ConversationQueue queue = ConversationDictionary.Instance.Lookup(msg.ConvId);
            if (queue != null)
            {
                queue.Enqueue(bytes);
            }
        }

        private void ConnectInfoResponse(byte[] bytes, IPEndPoint refEp)
        {
            //send ack
            ConnectInfo conv = ConversationFactory.Instance
                .CreateFromMessage<ConnectInfo>(bytes, refEp, null, null, null);
            conv.Start();
            //connect to server
            ConnectMsg msg = Message.Decode<ConnectMsg>(bytes);
            gameServer = msg.GameServer;
            //open tcp connection
            TCPClient tcp = new TCPClient();
            tcp.SetupConnection();

            int gamePort = tcp.port;
            
            tcpClients.Add(tcp);
            //send message with info on which port
            ConnectGameServer connectConv = ConversationFactory.Instance
                .CreateFromConversationType<ConnectGameServer>
                (gameServer, null, null, null);
            //connectConv._GameId = ;
            //connectConv._NumPlayers = ;
            connectConv._Port = gamePort;
            connectConv.Start();
        }

        private int GetOpenTCPPort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        private void LobbyHeartBeatResponse(byte[] bytes, IPEndPoint refEp)
        {
            LobbyHeartbeatConv conv = ConversationFactory.Instance
                .CreateFromMessage<LobbyHeartbeatConv>(bytes, refEp, null, null, null);
            conv.Start();
        }

        public override void TCPReceive()
        {
            byte[] bytes;
            foreach (TCPClient tcp in tcpClients)
            {
                bytes = tcp.Receive();
                if (bytes != null && bytes.Length > 0)
                {
                    RespondToMessage(bytes, null);
                    bytes = null;
                }
            }
        }
    }

    
}
