using System;
namespace Messages
{
    [Serializable]
    public class LobbyHeartbeat : Message
    {
        public int NumberOfPlayers { get; set; }
    }
}
