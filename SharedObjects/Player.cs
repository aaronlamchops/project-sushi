using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SharedObjects
{
    [Serializable]
    public class Player
    {
        public int id;
        public IPEndPoint playerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
        //hand
        public string name;
        public int score;
        public int puddingCount;
        public int gameId;
        //list cardsplayed

        public IPEndPoint GetIP()
        {
            return playerIP;
        }
    }
}
