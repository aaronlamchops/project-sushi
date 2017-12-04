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
        public int id { get; set; }
        public IPEndPoint playerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
        //hand
        public string name { get; set; }
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
