using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommSubSystem.ConversationClass;
using CommSubSystem.Conversations;
using log4net;
using CommSubSystem;
using System.Net;
using Messages;

using System.Diagnostics;

namespace LobbyApp
{
    public class LobbyReceive : Receiver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LobbyReceive));
        private static readonly object MyLock = new object();

        private int _GameID { get; set; }
        private Lobby GamesOnLobby;
        private short _Pid { get; set; }

        IPAddress gameServer { get; set; }

        public LobbyReceive()
        {
            _Pid = 1;
            GamesOnLobby = new Lobby();
            GamesOnLobby.HandleCreateGame(new SharedObjects.Player()
            {
                Id = 1468,
                Name = "Initial"
            }, 1, 5, "Initial Game", -1);
        }

        private short ManageProcessID()
        {
            _Pid++;
            return _Pid;
        }

        private int ManageGameID()
        {
            lock (MyLock)
            {
                _GameID++;
            }

            return _GameID;
        }

        protected override void ExecuteBasedOnType(byte[] bytes, TypeOfMessage type, IPEndPoint refEp)
        {
            switch (type)
            {
                case TypeOfMessage.CreateGame:
                    CreateGameResponse(bytes, refEp);
                    break;
                case TypeOfMessage.Registration:
                    RegistrationResponse(bytes, refEp);
                    break;

                case TypeOfMessage.RequestGameList:
                    RequestGameListResponse(bytes, refEp);
                    break;

                case TypeOfMessage.RequestGameListReply:
                    RequestGameListResponse(bytes, refEp);
                    break;
                case TypeOfMessage.StartGame:
                    StartGameResponse(bytes, refEp);
                    break;

                case TypeOfMessage.JoinGame:
                    JoinGameResponse(bytes, refEp);
                    break;
            }
        }

        private void StartGameResponse(byte[] bytes, IPEndPoint refEp)
        {
            //awknowlege request
            StartGame conv = ConversationFactory.Instance
                .CreateFromMessage<StartGame>(bytes, refEp, null, null, null);
            conv.Start();
            //send server location to all players
            StartGameMsg startMsg = Message.Decode<StartGameMsg>(bytes);
            int GameId = startMsg.GameId;
            GamesOnLobby.StartGame(GameId);
        }

        private void CreateGameResponse(byte[] bytes, IPEndPoint refEp)
        {
            CreateGameConv conv = ConversationFactory.Instance
                .CreateFromMessage<CreateGameConv>(bytes, refEp, null, null, null);
            conv._GameId = ManageGameID();

            CreateGame result = Message.Decode<CreateGame>(bytes);

            //add this game to the lobby list of games
            GamesOnLobby.HandleCreateGame(result.PlayerId, result.MinPlayers, result.MaxPlayers, result.GameName, conv._GameId);

            conv.Start();
        }

        private void RegistrationResponse(byte[] bytes, IPEndPoint refEp)
        {
            Registration conv = ConversationFactory.Instance
                .CreateFromMessage<Registration>(bytes, refEp, null, null, null);
            conv._processId = ManageProcessID();
            conv.Start();
        }

        private void RequestGameListResponse(byte[] bytes, IPEndPoint refEp)
        {
            RequestGameListConv conv = ConversationFactory.Instance
                .CreateFromMessage<RequestGameListConv>(bytes, refEp, null, null, null);
            conv._LobbyGameList = GamesOnLobby.gameList;
            conv.Start();
        }

        private void JoinGameResponse(byte[] bytes, IPEndPoint refEP)
        {
            JoinGameConv conv = ConversationFactory.Instance.CreateFromMessage<JoinGameConv>(bytes, refEP, null, null, null);

            JoinGame result = Message.Decode<JoinGame>(bytes);

            GamesOnLobby.HandleJoinGame(result.Player, result.GameID);

            conv._Game = GamesOnLobby.gameList[result.GameID];

            conv.Start();
        }
    }    
}
