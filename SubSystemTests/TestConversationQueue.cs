using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommSubSystem;
using Messages;
using SharedObjects;

namespace CommunicationSubsystemTest
{
    [TestClass]
    public class ConversationQueueTester
    {
        [TestMethod]
        public void EnqueueAndDequeue_1Envelope()
        {
            MessageId msgId = new MessageId();
            MessageId convId = new MessageId();
            ConversationQueue q = new ConversationQueue();
            ExitGame msg1 = new ExitGame() { MsgId = msgId, ConvId = convId, PlayerID = 1, GameID = 1 };
            PublicEndPoint p1 = new PublicEndPoint();
            Message msg = new Message();
            
            q.Enqueue(msg.Encode());
            byte[] result = q.Dequeue(1);
            Assert.AreEqual(msg.Encode(), result);
        }
      
    }
}
