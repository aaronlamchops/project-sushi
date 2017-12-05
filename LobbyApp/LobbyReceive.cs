using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommSubSystem.ConversationClass;
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
            Conversation conv = null;
            switch (type)
            {
                case TypeOfMessage.CreateGame:
                    conv = CreateGameResponse(bytes, refEp);
                    break;
                case TypeOfMessage.Registration:
                    conv = RegistrationResponse(bytes, refEp);
                    break;

                case TypeOfMessage.RequestGameList:
                    conv = RequestGameListResponse(bytes, refEp);
                    break;

                case TypeOfMessage.RequestGameListReply:
                    conv = RequestGameListResponse(bytes, refEp);
                    break;

                default:
                    conv = null;
                    break;

                case TypeOfMessage.JoinGame:
                    JoinGameResponse(bytes, refEp);
                    break;
            }
            if (conv != null)
            {
                Thread thrd = new Thread(conv.Execute);
                thrd.Start();
            }
        }

        private CreateGameConv CreateGameResponse(byte[] bytes, IPEndPoint refEp)
        {
            CreateGameConv conv = ConversationFactory.Instance.CreateFromMessage<CreateGameConv>(bytes, refEp, null, null, null);
            conv._GameId = ManageGameID();

            //add this game to the lobby list of games
            GamesOnLobby.HandleCreateGame(new SharedObjects.Player()
            { Id = conv._Player.Id }, conv._MinPlayers, conv._MaxPlayers, conv._GameName, conv._GameId);

            conv.Start();
        }

        private Registration RegistrationResponse(byte[] bytes, IPEndPoint refEp)
        {
            Registration conv = ConversationFactory.Instance.CreateFromMessage<Registration>(bytes, refEp, null, null, null);
            conv._processId = ManageProcessID();
            return conv;
        }

        private RequestGameListConv RequestGameListResponse(byte[] bytes, IPEndPoint refEp)
        {
            RequestGameListConv conv = ConversationFactory.Instance.CreateFromMessage<RequestGameListConv>(bytes, refEp, null, null, null);
            conv._LobbyGameList = GamesOnLobby.gameList;
            return conv;
        }

        private void JoinGameResponse(byte[] bytes, IPEndPoint refEP)
        {
            JoinGameConv conv = ConversationFactory.Instance.CreateFromMessage<JoinGameConv>(bytes, refEP, null, null, null);
            GamesOnLobby.HandleJoinGame(conv._Player, conv._GameId);
            conv.Start();
        }
    }    
}
