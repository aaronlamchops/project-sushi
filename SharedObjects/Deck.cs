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

    class Deck
    {
        private List<Card> Cards;
        private static Random rng = new Random();

        public Deck()
        {
            this.ResetDeck();
        }
        public void ResetDeck()
        {
            Cards = new List<Card>();
            foreach (CardTypes card in Enum.GetValues(typeof(CardTypes)))
            {
                int max = 0;
                switch (card.ToString())
                {
                    case "Wasabi": max = 6; break;
                    case "EggNigiri": max = 5; break;
                    case "SalmonNigiri": max = 10; break;
                    case "SquidNigiri": max = 5; break;
                    case "Sashimi": max = 14; break;
                    case "Tempura": max = 14; break;
                    case "Dumpling": max = 14; break;
                    case "MakiRoll": max = 26; break;
                    case "Pudding": max = 10; break;
                    case "Chopsticks": max = 4; break;
                }

                for (int y = 0; y < max; y++)
                {
                    Cards.Add(new Card() { CardId = (int)card, CardName = card.ToString() });
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
                Card tmp = Cards[k];
                Cards[k] = Cards[n];
                Cards[n] = tmp;
            }
        }
    }
}
