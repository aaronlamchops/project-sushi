using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    [Serializable]
    public class CreateGame : Message
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
    }
}
