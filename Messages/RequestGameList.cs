using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharedObjects;

namespace Messages
{
    [Serializable]
    public class RequestGameList : Message
    {
        public Lobby LobbyGameList;
    }
}
