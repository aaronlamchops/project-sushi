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

        public bool IsHost { get; set; }
        public bool InWaitingRoom { get; set; }

        public int ScoreCards()
        {
            int wasabi = PlayedCards.FindAll(x => x == CardTypes.Wasabi).Count;
            int egg = PlayedCards.FindAll(x => x == CardTypes.EggNigiri).Count;
            int salmon = PlayedCards.FindAll(x => x == CardTypes.SalmonNigiri).Count;
            int squid = PlayedCards.FindAll(x => x == CardTypes.SquidNigiri).Count;
            int sashimi = PlayedCards.FindAll(x => x == CardTypes.Sashimi).Count;
            int tempura = PlayedCards.FindAll(x => x == CardTypes.Tempura).Count;
            int dumpling = PlayedCards.FindAll(x => x == CardTypes.Dumpling).Count;
            int makiroll = PlayedCards.FindAll(x => x == CardTypes.MakiRoll).Count;
            int pudding = PlayedCards.FindAll(x => x == CardTypes.Pudding).Count;
            int Chopsticks = PlayedCards.FindAll(x => x == CardTypes.Chopsticks).Count;

            Score += egg;
            Score += salmon * 2;
            Score += squid * 3;
            Score += (sashimi / 3) * 10;
            Score += (tempura / 2) * 5;
            if (dumpling == 1) Score += 1;
            if (dumpling == 2) Score += 3;
            if (dumpling == 3) Score += 5;
            if (dumpling == 4) Score += 10;
            if (dumpling >= 5) Score += 15;

            return Score;
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

        public void SetIP(IPEndPoint ip)
        {
            playerIP = ip;
        }

        public IPEndPoint GetIP()
        {
            return playerIP;
        }
    }
}
