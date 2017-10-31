using System;
using Messages;
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
            Assert.AreEqual(msg1.MsgId, msg2.MsgId);
            Assert.AreEqual(msg1.MinPlayers, msg2.MinPlayers);
            Assert.AreEqual(msg1.MaxPlayers, msg2.MaxPlayers);
        }
    }
}