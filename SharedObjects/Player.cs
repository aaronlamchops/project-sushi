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
        public string Name { get; set; }
        public int Score { get; set; }
        public int PuddingCount { get; set; }
        public int GameId { get; set; }
        public List<CardTypes> Hand;
        public List<CardTypes> PlayedCards = new List<CardTypes>();

        public int ScoreCards()
        {
            //(TODO) scoring rules here
            return 0;
        }

        public Player() { }

        public Player(List<CardTypes> StartingHand)
        {
            Hand = StartingHand;
        }

        public void ChooseCard(SharedObjects.CardTypes card)
        {
            //not super safe yet
            Hand.Remove(card);
            PlayedCards.Add(card);
        }

        public IPEndPoint GetIP()
        {
            return playerIP;
        }
    }
}
