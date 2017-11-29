using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SharedObjects
{
    public class Lobby
    {
        ConcurrentDictionary<int,Game> gameList = new ConcurrentDictionary<int, Game>();
        private int IDCounter = 1;//lobby ID is always 1
        public volatile bool isRunning = true;

        public int GetID() 
        {
            IDCounter += 1;
            return IDCounter;
        }

        public void HandleRequestGameList()
        {
            //TODO Send gameList to player
        }

        public void HandleCreateGame(Player host, int minPlayer, int maxPlayer)
        {
            int gameID = -1;//TODO Send RequestGameID to gameServer to get valid ID
            Game g = new Game(gameID, host, minPlayer, maxPlayer);
            gameList[gameID] = g;
        }

        public void HandleJoinGame(Player p, int gameID) {
            Game g = gameList[gameID];
            g.AddPlayer(p);
            gameList[gameID] = g;
        }

        public void StartGame(int gameID)
        {
            Game g = gameList[gameID];
            foreach(Player p in g.playerList)
            {
                //TODO Send StartGame message to p
            }
            gameList.TryRemove(gameID, out g);
        }

        public void SendHeartbeats()
        {
            while(isRunning)
            {
                //TODO hearbeat all players that have joined games
                //If timeout, remove player from game
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
