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
        public List<Game> GameList { get; set; }
    }

    public class Game
    {
        public int gameId { get; set; }
        public string GameName { get; set; }
        // list players;
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        //host player
    }
}
