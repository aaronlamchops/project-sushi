using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedObjects;

namespace Messages
{
    [Serializable]
    public class CreateGame : Message
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string GameName { get; set; }
        public Player PlayerId { get; set; }
    }

    [Serializable]
    public class CreateGameReply : Message
    {
        public int GameId { get; set; }
    }
}
