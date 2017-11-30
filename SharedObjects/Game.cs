using System;
using System.Collections.Generic;


namespace SharedObjects
{
    [Serializable]
    public class Game
    {
        public int gameId;
        public List<Player> playerList = new List<Player>();
        public int MinPlayers;
        public int MaxPlayers;
        public Player host;

        public Game(int gameId, Player host, int minPlayer, int maxPlayer)
        {
            this.gameId = gameId;
            this.host = host;
            this.MinPlayers = minPlayer;
            this.MaxPlayers = maxPlayer;
        }
        public void AddPlayer(Player p)
        {
            if (playerList.Count < MaxPlayers)
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
