using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public enum CardTypes
    {
        Wasabi,
        EggNigiri,
        SalmonNigiri,
        SquidNigiri,
        Sashimi,
        Tempura,
        Dumpling,
        MakiRoll,
        Pudding,
        Chopsticks
    }

    public class Deck
    {
        public List<CardTypes> Cards;
        private static Random rng = new Random();

        public Deck()
        {
            this.ResetDeck();
        }

        public void ResetDeck()
        {
            Cards = new List<CardTypes>();
            foreach (CardTypes card in Enum.GetValues(typeof(CardTypes)))
            {
                int max = 0;
                switch (card)
                {
                    case CardTypes.Wasabi: max = 6; break;
                    case CardTypes.EggNigiri: max = 5; break;
                    case CardTypes.SalmonNigiri: max = 10; break;
                    case CardTypes.SquidNigiri: max = 5; break;
                    case CardTypes.Sashimi: max = 14; break;
                    case CardTypes.Tempura: max = 14; break;
                    case CardTypes.Dumpling: max = 14; break;
                    case CardTypes.MakiRoll: max = 26; break;
                    case CardTypes.Pudding: max = 10; break;
                    case CardTypes.Chopsticks: max = 4; break;
                }

                for (int y = 0; y < max; y++)
                {
                    Cards.Add(card);
                }
            }
        }

        public void ShuffleDeck()
        {
            int n = Cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CardTypes tmp = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = tmp;
            }
        }
        
        public List<CardTypes> DealHand(int handSize)
        {
            List<CardTypes> hand = new List<CardTypes>();
            int random;
            for (int i = 0; i < handSize; i++)
            {
                random = rng.Next(Cards.Count);
                hand.Add(Cards[random]);
                Cards.RemoveAt(random);
            }
            return hand;
        }
    }
}
