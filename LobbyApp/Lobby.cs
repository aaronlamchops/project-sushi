using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Net;
using CommSubSystem.Conversation;


namespace SharedObjects
{
    public class Lobby
    {
        ConcurrentDictionary<int,Game> gameList = new ConcurrentDictionary<int, Game>();
        private int IDCounter = 1;//lobby ID is always 1
        public volatile bool isRunning = true;

        public int GetID()//before response
        {
            IDCounter += 1;
            return IDCounter;
        }

        public void HandleRequestGameList()
        {
            //TODO Send gameList to player

        }

        public void HandleCreateGame(Player host, int minPlayer, int maxPlayer)//before response 
        {
            int gameID = -1;//TODO Send RequestGameID to gameServer to get valid ID
            Game g = new Game(gameID, host, minPlayer, maxPlayer);
            gameList[gameID] = g;
        }

        public void HandleJoinGame(Player p, int gameID) {//before response
            Game g = gameList[gameID];
            g.AddPlayer(p);
            gameList[gameID] = g;
        }

        public void StartGame(int gameID)//before response
        {
            Game g = gameList[gameID];
            foreach(Player p in g.playerList)
            {
                //TODO Send StartGame message to p
                IPEndPoint playerIP = p.GetIP();
                StartGameConv conv =
                    ConversationFactory.Instance
                    .CreateFromConversationType<StartGameConv>
                    (playerIP, null, null);//pass in numberOfPlayers somehow?
                Thread convThread = new Thread(conv.Execute);
                convThread.Start();
            }
            gameList.TryRemove(gameID, out g);
        }

        public void SendHeartbeats()//run as thread
        {
            while(isRunning)
            {
                //TODO hearbeat to all players that have joined games
                foreach(var keypair in gameList)
                {
                    Game g = keypair.Value;
                    int numberOfPlayers = g.playerList.Count();
                    foreach (Player p in g.playerList)
                    {
                        IPEndPoint playerIP = p.GetIP();
                        LobbyHeartbeatConv conv =
                            ConversationFactory.Instance
                            .CreateFromConversationType<LobbyHeartbeatConv>
                            (playerIP, null, null);//pass in numberOfPlayers somehow?
                        Thread convThread = new Thread(conv.Execute);
                        convThread.Start();
                        //If timeout, remove player from game
                    }
                }


            }
        }

    }

    public class Game
    {
        int gameId;
        public List<Player> playerList = new List<Player>();
        int MinPlayers;
        int MaxPlayers;
        Player host;

        public Game(int gameId, Player host, int minPlayer, int maxPlayer){
            this.gameId = gameId;
            this.host = host;
            this.MinPlayers = minPlayer;
            this.MaxPlayers = maxPlayer;
        }
        public void AddPlayer(Player p) 
        {
            if (playerList.Count() < MaxPlayers)
            {
                playerList.Add(p);
            }
            else 
            {
                //error, maxplayers exceeded
            }
        }
    }
}
