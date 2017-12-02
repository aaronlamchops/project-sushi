using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Messages
{
    [Serializable]
    public class ConnectMsg : Message
    {
        public IPEndPoint GameServer { get; set; }
    }

    [Serializable]
    public class StartGameMsg : Message
    {
        public int GameId { get; set; }
    }

    [Serializable]
    public class ConnectGSMsg : Message
    {
        public int GameId { get; set; }
        public int Players { get; set; }
        public int Port { get; set; }
    }
}
