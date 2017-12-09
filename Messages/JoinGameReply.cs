using System;
using SharedObjects;

namespace Messages
{
    [Serializable]
    public class JoinGameReply : Message
    {
        public Game Game { get; set; }
        //public int PlayerID { get; set; }
    }
}
