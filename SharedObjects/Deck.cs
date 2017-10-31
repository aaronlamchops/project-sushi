using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    enum CardTypes
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
                if (card.ToString() == "Wasabi") { max = 6; }
                else if (card.ToString() == "EggNigiri") { max = 5; }
                else if (card.ToString() == "SalmonNigiri") { max = 10; }
                else if (card.ToString() == "SquidNigiri") { max = 5; }
                else if (card.ToString() == "Sashimi") { max = 14; }
                else if (card.ToString() == "Tempura") { max = 14; }
                else if (card.ToString() == "Dumpling") { max = 14; }
                else if (card.ToString() == "MakiRoll") { max = 26; }
                else if (card.ToString() == "Pudding") { max = 10; }
                else if (card.ToString() == "Chopsticks") { max = 4; }

                for (int y = 0; y < max; y++)
                {
                    Cards.Add(new Card() { CardId = (int)card, CardName = card.ToString() });
                }
            }
        }
        public void ShuffleDeck()
        {

        }
    }
}
