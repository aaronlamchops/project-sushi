using System;
namespace Messages
{
    [Serializable]
    public class JoinGameReply : Message
    {
        public int GameID { get; set; }
        //public int PlayerID { get; set; }
    }
}
