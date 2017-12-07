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
        public int Id { get; set; }
        private IPEndPoint playerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
        //hand
        public string Name { get; set; }
        public int Score { get; set; }
        public int PuddingCount { get; set; }
        public int GameId { get; set; }
        public bool IsHost { get; set; }
        public bool InWaitingRoom { get; set; }
        //list cardsplayed

        public IPEndPoint GetIP()
        {
            return playerIP;
        }
    }
}
