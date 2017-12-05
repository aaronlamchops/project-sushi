using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Net;
using CommSubSystem.ConversationClass;
using CommSubSystem.Conversations;
using SharedObjects;


namespace LobbyApp
{
    public class Lobby
    {
        public ConcurrentDictionary<int,Game> gameList = new ConcurrentDictionary<int, Game>();
        private int IDCounter = 1;//lobby ID is always 1
        public volatile bool isRunning = true;

        public int GetPlayerID()//before response
        {
            //add lock
            IDCounter += 1;
            return IDCounter;
        }

        public void HandleRequestGameList()
        {
            //TODO Send gameList to player

        }

        public void HandleCreateGame(Player host, int minPlayer, int maxPlayer, string name, int gameID)//before response 
        {
            //int gameID = -1;//TODO Send RequestGameID to gameServer to get valid ID
            Game g = new Game(gameID, host, minPlayer, maxPlayer, name);
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
                //Send gameserver info to each player
                IPEndPoint playerIP = p.GetIP();
                ConnectInfo conv =
                    ConversationFactory.Instance
                    .CreateFromConversationType<ConnectInfo>
                    (playerIP, null, null, null);
                conv._gameServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1026);
                conv.Start();
            }
            gameList.TryRemove(gameID, out g);
        }

        public void SendHeartbeats()//run as seperate thread
        {
            while(isRunning)
            {
                //hearbeat to all players that have joined games
                foreach(var keypair in gameList)
                {
                    Game g = keypair.Value;
                    int numberOfPlayers = g.playerList.Count;
                    foreach (Player p in g.playerList)
                    {
                        IPEndPoint playerIP = p.GetIP();
                        LobbyHeartbeatConv conv =
                            ConversationFactory.Instance
                            .CreateFromConversationType<LobbyHeartbeatConv>
                            (playerIP, null, null, null);
                        conv._NumberOfPlayers = numberOfPlayers;
                        Thread convThread = new Thread(conv.Execute);
                        convThread.Start();
                        //If timeout, remove player from game
                    }
                }


            }
        }

    }
}
