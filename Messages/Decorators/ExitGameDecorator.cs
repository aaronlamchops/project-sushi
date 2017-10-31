using System;
namespace Messages.Decorators
{
    [Serializable]
    public class ExitGame : Message
    {
        public int GameID { get; set; }
        public int PlayerID { get; set; }
    }
}
