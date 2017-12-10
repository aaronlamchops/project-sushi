using System;
using SharedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SharedObjectsTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestPlayerScoring()
        {
            List<CardTypes> hand = new List<CardTypes>()
            {
                CardTypes.Pudding,
                CardTypes.EggNigiri,
                CardTypes.Sashimi,
                CardTypes.Sashimi,
                CardTypes.Sashimi
            };
            Player player = new Player(hand);
            Assert.IsTrue(player.ScoreCards() == 11);
            List<CardTypes> hand2 = new List<CardTypes>()
            {
                CardTypes.Pudding,
                CardTypes.SquidNigiri,
                CardTypes.Dumpling,
                CardTypes.Dumpling,
                CardTypes.Sashimi
            };
            Player player2 = new Player(hand2);
            Assert.IsTrue(player.ScoreCards() == 4);
            List<CardTypes> hand3 = new List<CardTypes>()
            {
                CardTypes.SquidNigiri,
                CardTypes.EggNigiri,
                CardTypes.SalmonNigiri,
                CardTypes.Tempura,
                CardTypes.Tempura
            };
            Player player3 = new Player(hand3);
            Assert.IsTrue(player.ScoreCards() == 11);
        }

        [TestMethod]
        public void TestPlayerAddCards()
        {
            List<CardTypes> hand = new List<CardTypes>()
            {
                CardTypes.Pudding,
                CardTypes.EggNigiri,
                CardTypes.Sashimi
            };
            Player player = new Player(hand);
            player.ChooseCard(CardTypes.Sashimi);

            Assert.IsTrue(player.Hand.Contains(CardTypes.Pudding));
            Assert.IsTrue(player.Hand.Contains(CardTypes.EggNigiri));
            Assert.IsFalse(player.Hand.Contains(CardTypes.Sashimi));
        }
    }

    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void TestResetDeck()
        {
            Deck deck = new Deck();
            deck.ResetDeck();
            Assert.IsTrue(deck.Cards.Count == 108);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.Wasabi).Count == 6);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.EggNigiri).Count == 5);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.SalmonNigiri).Count == 10);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.SquidNigiri).Count == 5);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.Sashimi).Count == 14);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.Tempura).Count == 14);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.Dumpling).Count == 6);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.MakiRoll).Count == 26);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.Pudding).Count == 10);
            Assert.IsTrue(deck.Cards.FindAll(x => x == CardTypes.Chopsticks).Count == 4);
        }

        [TestMethod]
        public void TestDealHand()
        {
            Deck deck = new Deck();
            deck.ResetDeck();
            List<CardTypes> hand = deck.DealHand(10);
            Assert.IsTrue(hand.Count == 10);
            List<CardTypes> hand2 = deck.DealHand(10);
            Assert.IsFalse(hand.Equals(hand2));
        }

        [TestMethod]
        public void TestShuffleDeck()
        {
            Deck deck = new Deck();
            List<CardTypes> list = deck.Cards;
            deck.ShuffleDeck();
            Assert.IsFalse(list.Equals(deck.Cards));
        }
    }
}
