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
                case TypeOfMessage.PassCard:
                    PassCardResponse(bytes, refEp);
                    break;
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
            int gamePort = 500;
            tcp.SetupConnection(gamePort);
            tcpClients.Add(tcp);
            //send message with info on which port
            ConnectGameServer connectConv = ConversationFactory.Instance
                .CreateFromMessage<ConnectGameServer>
                (bytes, gameServer, null, null, null);
            //connectConv._GameId = ;
            //connectConv._NumPlayers = ;
            connectConv._Port = gamePort;
        }
        private void LobbyHeartBeatResponse(byte[] bytes, IPEndPoint refEp)
        {
            LobbyHeartbeatConv conv = ConversationFactory.Instance
                .CreateFromMessage<LobbyHeartbeatConv>(bytes, refEp, null, null, null);
            conv.Start();
        }

        public delegate void PassCardsToGameHandler(List<CardTypes> passedHand);
        public PassCardsToGameHandler PassCardsToGame { get; set; }

        private void PassCardResponse(byte[] bytes, IPEndPoint refEp)
        {
            PassCard msg = Message.Decode<PassCard>(bytes);

            PassCardsToGame(msg.Hand);      //calls the delegate and sends cards to game gui
            //do something with the cards
        }

        public override void TCPReceive()
        {
            byte[] bytes;
            foreach (TCPClient tcp in tcpClients)
            {
                bytes = tcp.Receive();
                if (bytes != null)
                {
                    RespondToMessage(bytes, null);
                    bytes = null;
                }
            }
        }
    }

    
}
