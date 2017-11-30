using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommSubSystem;
using Messages;
using SharedObjects;

namespace CommunicationSubsystemTest
{
    [TestClass]
    public class ConversationDictonaryTester
    {
        [TestMethod]
        public void CreateQueueNull()
        {
            MessageId msg = null;
            Assert.IsNull(ConversationDictionary.Instance.CreateQueue(msg));
        }
        [TestMethod]
        public void CreateQueue()
        {
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            Assert.IsNotNull(ConversationDictionary.Instance.CreateQueue(msg));
        }
        [TestMethod]
        public void Lookup()
        {
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            ConversationDictionary.Instance.CreateQueue(msg);
            Assert.IsInstanceOfType(ConversationDictionary.Instance.Lookup(msg),
                typeof(ConversationQueue));
        }
        [TestMethod]
        public void Close()
        {
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            ConversationDictionary.Instance.CreateQueue(msg);
            ConversationDictionary.Instance.CloseQueue(msg);
            Assert.IsNull(ConversationDictionary.Instance.Lookup(msg));
        }
        [TestMethod]
        public void ClearAll()
        {
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            MessageId msg2 = new MessageId() { Pid = 2, Seq = 1 };
            ConversationDictionary.Instance.CreateQueue(msg);
            ConversationDictionary.Instance.CreateQueue(msg2);
            ConversationDictionary.Instance.ClearAllQueues();
            Assert.IsNull(ConversationDictionary.Instance.Lookup(msg));
            Assert.IsNull(ConversationDictionary.Instance.Lookup(msg2));
        }
        [TestMethod]
        public void SetupConversation()
        {
            MessageId msgid = new MessageId() { Pid = 1, Seq = 1 };
            Message msg = new Message()
            {
                MsgId = msgid,
                ConvId = msgid
            };
            ConversationQueue queue = ConversationDictionary.Instance.SetupConversation(msgid, msg.Encode());
            Assert.AreEqual(queue.QueueID, msgid);
        }


    }
}
