using System;
using SharedObjects;
namespace Messages
{
    [Serializable]
    public class SelectCard : Message
    {
        public CardTypes CardID { get; set; }
        public int PlayerID { get; set; }
        public int GameId { get; set; }
    }
}
