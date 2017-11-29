using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Lobby
    {
        //list of games
        public List<Game> GameList;
    }

    public class Game
    {
        int gameId;
        // list players;
        int MinPlayers;
        int MaxPlayers;
        //host player
    }
}
