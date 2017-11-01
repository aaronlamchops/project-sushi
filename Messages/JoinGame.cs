using System;
namespace Messages
{
    [Serializable]
    public class JoinGame : Message
    {
        public int GameID { get; set; }
        public int PlayerID { get; set; }
    }
}
