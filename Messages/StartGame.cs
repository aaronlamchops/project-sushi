using System;
namespace Messages
{
    [Serializable]
    public class StartGame : Message
    {
        public int GameID { get; set; }
    }
}
