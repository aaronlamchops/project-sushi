using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class Lobby
    {
        List<Game> gameList = new List<Game>();
    }

    public class Game
    {
        int gameId;
        List<Player> playerList = new List<Player>();
        int MinPlayers;
        int MaxPlayers;
        Player host;
    }
}
