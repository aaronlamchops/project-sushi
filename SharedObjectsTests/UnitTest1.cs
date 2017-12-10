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

        }

        [TestMethod]
        public void TestDealHand()
        {

        }

        [TestMethod]
        public void TestShuffleDeck()
        {
            
        }
    }

    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void AddPlayerTest()
        {

        }

        [TestMethod]
        public void SelectCardTest()
        {

        }

        [TestMethod]
        public void StartGameTest()
        {

        }

        [TestMethod]
        public void PassCardsTest()
        {

        }
    }
}
