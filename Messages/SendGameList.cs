using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedObjects;

namespace Messages
{
    [Serializable]
    public class SendGameList : Message
    {
        public List<Game> gameList { get; set; } 
    }
}
