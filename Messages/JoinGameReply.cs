using System;
using SharedObjects;

namespace Messages
{
    [Serializable]
    public class JoinGameReply : Message
    {
        public GameInfo Game { get; set; }
        //public int PlayerID { get; set; }
    }
}
