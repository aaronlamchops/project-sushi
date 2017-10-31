using System;
using Messages;
using SharedObjects;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageTest
{
    [TestClass]
    public class MessageTester
    {
        [TestMethod]
        public void EncodeAndDecode_CreateGame()
        {
            CreateGame msg1 = new CreateGame() { MsgId = 1 , ConvId = 1, MinPlayers = 1, MaxPlayers = 5};
            byte[] bytes = msg1.Encode();

            CreateGame msg2 = Message.Decode(bytes) as CreateGame;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.MinPlayers, msg2.MinPlayers);
            Assert.AreEqual(msg1.MaxPlayers, msg2.MaxPlayers);
        }

        [TestMethod]
        public void EncodeAndDecode_ExitGame()
        {
            ExitGame msg1 = new ExitGame() { MsgId = 1, ConvId = 1, PlayerID = 2 , GameID = 5 };
            byte[] bytes = msg1.Encode();

            ExitGame msg2 = Message.Decode(bytes) as ExitGame;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.GameID, msg2.GameID);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
        }

        [TestMethod]
        public void EncodeAndDecode_Heartbeat()
        {
            Heartbeat msg1 = new Heartbeat() { MsgId = 1, ConvId = 1};
            byte[] bytes = msg1.Encode();

            Heartbeat msg2 = Message.Decode(bytes) as Heartbeat;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
        }

        [TestMethod]
        public void EncodeAndDecode_JoinGame()
        {
            ExitGame msg1 = new ExitGame() { MsgId = 1, ConvId = 1, PlayerID = 2, GameID = 5 };
            byte[] bytes = msg1.Encode();

            ExitGame msg2 = Message.Decode(bytes) as ExitGame;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.GameID, msg2.GameID);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
        }

        [TestMethod]
        public void EncodeAndDecode_SelectCard()
        {
            SelectCard msg1 = new SelectCard() { MsgId = 1, ConvId = 1, PlayerID = 2, CardID = CardTypes.Pudding };
            byte[] bytes = msg1.Encode();

            SelectCard msg2 = Message.Decode(bytes) as SelectCard;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.CardID, msg2.CardID);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
        }

        [TestMethod]
        public void EncodeAndDecode_StartGame()
        {
            StartGame msg1 = new StartGame() { MsgId = 1, ConvId = 1, GameID = 7};
            byte[] bytes = msg1.Encode();

            StartGame msg2 = Message.Decode(bytes) as StartGame;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.GameID, msg2.GameID);
        }

        [TestMethod]
        public void EncodeAndDecode_UserInfo()
        {
            UserInfo msg1 = new UserInfo() { MsgId = 1, ConvId = 1, UserName = "Kylie" };
            byte[] bytes = msg1.Encode();

            UserInfo msg2 = Message.Decode(bytes) as UserInfo;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.UserName, msg2.UserName);
        }

        [TestMethod]
        public void EncodeAndDecode_UpdateChat()
        {
            UpdateChat msg1 = new UpdateChat() { MsgId = 1, ConvId = 1, PlayerID = 7, Message = "New game who dis?" };
            byte[] bytes = msg1.Encode();

            UpdateChat msg2 = Message.Decode(bytes) as UpdateChat;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
            Assert.AreEqual(msg1.Message, msg2.Message);
        }

        [TestMethod]
        public void EncodeAndDecode_PassCards()
        {
            List<CardTypes> cards = new List<CardTypes>();
            cards.Add(CardTypes.Sashimi);
            cards.Add(CardTypes.EggNigiri);
            PassCard msg1 = new PassCard() { MsgId = 1, ConvId = 1, Hand = cards};
            byte[] bytes = msg1.Encode();

            PassCard msg2 = Message.Decode(bytes) as PassCard;
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            for(int i = 0; i < cards.Count; i++)
            {
                Assert.AreEqual(msg1.Hand[i], msg2.Hand[i]);
            }
            
        }
    }
}