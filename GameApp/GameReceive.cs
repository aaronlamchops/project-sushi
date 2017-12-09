using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommSubSystem;
using CommSubSystem.ConversationClass;
using CommSubSystem.Conversations;
using Messages;
using SharedObjects;

namespace GameApp
{
    public class GameReceive : Receiver
    {
        protected Dictionary<int, TCPClient> tcpClients = new Dictionary<int, TCPClient>();
        private List<Game> gameList = new List<Game>();

        protected override void ExecuteBasedOnType
            (byte[] bytes, TypeOfMessage type, IPEndPoint refEp)
        {
            switch (type)
            {
                case TypeOfMessage.ConnectGameServerMsg:
                    ConnectGameServerResponse(bytes, refEp);
                    break;
                case TypeOfMessage.SelectCard:
                    SelectCardResponse(bytes);
                    break;
            }
        }

        public void ConnectGameServerResponse(byte[] bytes, IPEndPoint refEp)
        {
            ConnectGameServer conv = ConversationFactory.Instance.
                CreateFromMessage<ConnectGameServer>(bytes, refEp, null, null, null);
            conv.Start();

            ConnectGSMsg convMessage = Message.Decode<ConnectGSMsg>(bytes);
            //generate TCP client
            TCPClient client = new TCPClient();
            IPEndPoint clientTcp = refEp;
            clientTcp.Port = convMessage.Port;
            client.ConnectToServer(clientTcp);
            int playerId = convMessage.PlayerId;
            //need to store client tcp somewhere
            tcpClients.Add(playerId, client);

            //add player to game
            Game game;
            //find game in list
            if(gameList.Exists(x => x.gameId == convMessage.GameId))
            {
                game = gameList.Find(x => x.gameId == convMessage.GameId);
            }
            else
            {
                game = new Game(convMessage.GameId, convMessage.Players, SendCard);
            }
            game.AddPlayer(playerId);
        }

        public void SendCard(int playerId, List<CardTypes> hand)
        {
            PassCards conv = ConversationFactory.Instance.CreateFromConversationType<PassCards>
                (null, null, null, null);
            conv.tcpClient = tcpClients[playerId];
            conv._Hand = hand;
            conv.Start();
        }

        public void SelectCardResponse(byte[] bytes)
        {
            //no response is required so conversation is finished
            SelectCard msg = Message.Decode<SelectCard>(bytes);

            Game game = gameList.Find(x => x.gameId == msg.GameId);
            game.SelectCard(msg.PlayerID, msg.CardID);
        }

        public override void TCPReceive()
        {
            byte[] bytes;
            foreach (TCPClient tcp in tcpClients.Values)
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
