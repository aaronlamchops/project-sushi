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
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            CreateGame msg1 = new CreateGame() { MsgId = msgId , ConvId = convId, MinPlayers = 1, MaxPlayers = 5};
            byte[] bytes = msg1.Encode();

            CreateGame msg2 = Message.Decode<CreateGame>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.MinPlayers, msg2.MinPlayers);
            Assert.AreEqual(msg1.MaxPlayers, msg2.MaxPlayers);
        }

        [TestMethod]
        public void EncodeAndDecode_ExitGame()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            ExitGame msg1 = new ExitGame() { MsgId = msgId, ConvId = convId, PlayerID = 2 , GameID = 5 };
            byte[] bytes = msg1.Encode();

            ExitGame msg2 = Message.Decode<ExitGame>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.GameID, msg2.GameID);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
        }

        [TestMethod]
        public void EncodeAndDecode_Heartbeat()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            Heartbeat msg1 = new Heartbeat() { MsgId = msgId, ConvId = convId};
            byte[] bytes = msg1.Encode();

            Heartbeat msg2 = Message.Decode<Heartbeat>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
        }

        [TestMethod]
        public void EncodeAndDecode_JoinGame()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            ExitGame msg1 = new ExitGame() { MsgId = msgId, ConvId = convId, PlayerID = 2, GameID = 5 };
            byte[] bytes = msg1.Encode();

            ExitGame msg2 = Message.Decode<ExitGame>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.GameID, msg2.GameID);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
        }

        [TestMethod]
        public void EncodeAndDecode_SelectCard()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            SelectCard msg1 = new SelectCard() { MsgId = msgId, ConvId = convId, PlayerID = 2, CardID = CardTypes.Pudding };
            byte[] bytes = msg1.Encode();

            SelectCard msg2 = Message.Decode<SelectCard>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.CardID, msg2.CardID);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
        }

        [TestMethod]
        public void EncodeAndDecode_StartGame()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            StartGameMsg msg1 = new StartGameMsg()
            { MsgId = msgId, ConvId = convId};
            byte[] bytes = msg1.Encode();

            StartGameMsg msg2 = Message.Decode<StartGameMsg>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
        }

        [TestMethod]
        public void EncodeAndDecode_UserInfo()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            UserInfo msg1 = new UserInfo() { MsgId = msgId, ConvId = convId, UserName = "Kylie" };
            byte[] bytes = msg1.Encode();

            UserInfo msg2 = Message.Decode<UserInfo>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.UserName, msg2.UserName);
        }

        [TestMethod]
        public void EncodeAndDecode_UpdateChat()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            UpdateChat msg1 = new UpdateChat() { MsgId = msgId, ConvId = convId, PlayerID = 7, Message = "New game who dis?" };
            byte[] bytes = msg1.Encode();

            UpdateChat msg2 = Message.Decode<UpdateChat>(bytes);
            Assert.IsNotNull(msg2);
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.ConvId, msg2.ConvId);
            Assert.AreEqual(msg1.PlayerID, msg2.PlayerID);
            Assert.AreEqual(msg1.Message, msg2.Message);
        }

        [TestMethod]
        public void EncodeAndDecode_PassCards()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            List<CardTypes> cards = new List<CardTypes>();
            cards.Add(CardTypes.Sashimi);
            cards.Add(CardTypes.EggNigiri);
            PassCard msg1 = new PassCard() { MsgId = msgId, ConvId = convId, Hand = cards};
            byte[] bytes = msg1.Encode();

            PassCard msg2 = Message.Decode<PassCard>(bytes);
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