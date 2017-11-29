using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedObjects;
using System.Collections.Concurrent;

namespace Messages
{
    [Serializable]
    public class SendGameList : Message
    {
        public ConcurrentDictionary<int, Game> gameList { get; set; } 
    }
}
