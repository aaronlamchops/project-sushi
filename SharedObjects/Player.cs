using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SharedObjects
{
    public class Player
    {
        int id;
        IPEndPoint playerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
        //hand
        string name;
        int score;
        int puddingCount;
        //list cardsplayed

        public IPEndPoint GetIP()
        {
            return playerIP;
        }
    }
}
