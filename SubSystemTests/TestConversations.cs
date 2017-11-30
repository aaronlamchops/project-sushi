using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommSubSystem.ConversationClass;
using SharedObjects;
using Messages;
using System.Net;

namespace SubSystemTests
{
    [TestClass]
    public class TestConversations
    {
        [TestMethod]
        public void TestCreateConversation()
        {
            MessageId msgid = new MessageId() { Pid = 7, Seq = 3 };
            Message msg = new Message()
            {
                ConvId = msgid,
                MsgId = msgid,
                MessageType = TypeOfMessage.Ack
            };
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);

            CreateGameConv conv =
                ConversationFactory
                .Instance.CreateFromMessage<CreateGameConv>
                (msg.Encode(), ip, null, null, null);

            Assert.AreEqual(conv.EndIP, ip);
            Assert.AreEqual(conv.ConvId, msgid);

            CreateGameConv conv2 =
                ConversationFactory
                .Instance.CreateFromConversationType<CreateGameConv>
                (ip, null, null, null);

            Assert.AreEqual(conv2.EndIP, ip);
            Assert.AreNotEqual(conv.ConvId, msgid);
        }

        [TestMethod]
        public void TestCreateGameConv()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            CreateGameConv conv =
                ConversationFactory
                .Instance.CreateFromConversationType<CreateGameConv>
                (ip, null, null, null);
            conv._MinPlayers = 5;
            conv._MaxPlayers = 6;

            CreateGame msg = (CreateGame) conv.CreateFirstMessage();
            Assert.AreEqual(msg.MaxPlayers, 6);
            Assert.AreEqual(msg.MinPlayers, 5);

            CommSubSystem.ConversationQueue queue = conv.MyQueue;
            queue.Enqueue(msg.Encode());

            conv.Receive();

            Assert.AreEqual(conv.Error, null);
            Assert.AreEqual(conv.incomingMsg, msg.Encode());
            Assert.AreEqual(true, conv.ValidateEnvelope(msg));


        }
    }
}
