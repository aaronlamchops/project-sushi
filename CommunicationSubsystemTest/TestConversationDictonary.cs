using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunicationSubsystem;
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
            ConversationDictionary conDic = new ConversationDictionary();
            MessageId msg = null;
            Assert.IsNull(conDic.CreateQueue(msg));
        }
        [TestMethod]
        public void CreateQueue()
        {
            ConversationDictionary conDic = new ConversationDictionary();
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            Assert.IsNotNull(conDic.CreateQueue(msg));
        }
        [TestMethod]
        public void Lookup()
        {
            ConversationDictionary conDic = new ConversationDictionary();
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            conDic.CreateQueue(msg);
            Assert.IsNotFalse(conDic.Lookup(msg));
        }
        [TestMethod]
        public void ClearAllQueues()
        {
            ConversationDictionary conDic = new ConversationDictionary();
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            conDic.CreateQueue(msg);
            conDic.ClearAllQueues();
            Assert.IsFalse(conDic.Lookup(msg));
        }
        [TestMethod]
        public void CloseQueue()
        {
            ConversationDictionary conDic = new ConversationDictionary();
            MessageId msg = new MessageId() { Pid = 1, Seq = 1 };
            conDic.CreateQueue(msg);
            conDic.CloseQueue(msg);
            Assert.IsFalse(conDic.Lookup(msg));
        }

    }
}
