using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommunicationSubsystem;
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
            ConversationQueue q = new ConversationQueue();
            ExitGame msg1 = new ExitGame() { MsgId = 1, ConvId = 1, PlayerID = 1, GameID = 1 };
            PublicEndPoint p1 = new PublicEndPoint();
            Envelope e1 = new Envelope(msg1, p1);
            q.Enqueue(e1);
            Envelope result = q.Dequeue(1);
            Assert.AreEqual(e1.MessageToBeSent, result.MessageToBeSent);
            Assert.AreEqual(e1.EndPoint, result.EndPoint);
        }
        [TestMethod]
        public void EnqueueAndDequeue_2Envelopes()
        {
            ConversationQueue q = new ConversationQueue();
            ExitGame msg1 = new ExitGame() { MsgId = 1, ConvId = 1, PlayerID = 1, GameID = 1 };
            ExitGame msg2 = new ExitGame() { MsgId = 2, ConvId = 2, PlayerID = 2, GameID = 2 };
            PublicEndPoint p1 = new PublicEndPoint();
            Envelope e1 = new Envelope(msg1, p1);
            Envelope e2 = new Envelope(msg2, p1);
            q.Enqueue(e1);
            q.Enqueue(e2);
            Envelope result = q.Dequeue(1);
            Assert.AreEqual(e1.MessageToBeSent, result.MessageToBeSent);
            Assert.AreEqual(e1.EndPoint, result.EndPoint);
            Envelope result2 = q.Dequeue(1);
            Assert.AreEqual(e2.MessageToBeSent, result2.MessageToBeSent);
            Assert.AreEqual(e2.EndPoint, result2.EndPoint);
        }
        [TestMethod]
        public void EnqueueAndDequeue_3Envelopes()
        {
            ConversationQueue q = new ConversationQueue();
            ExitGame msg1 = new ExitGame() { MsgId = 1, ConvId = 1, PlayerID = 1, GameID = 1 };
            ExitGame msg2 = new ExitGame() { MsgId = 2, ConvId = 2, PlayerID = 2, GameID = 2 };
            ExitGame msg3 = new ExitGame() { MsgId = 3, ConvId = 3, PlayerID = 3, GameID = 3 };
            PublicEndPoint p1 = new PublicEndPoint();
            Envelope e1 = new Envelope(msg1, p1);
            Envelope e2 = new Envelope(msg2, p1);
            Envelope e3 = new Envelope(msg3, p1);
            q.Enqueue(e1);
            q.Enqueue(e2);
            q.Enqueue(e3);
            Envelope result = q.Dequeue(1);
            Assert.AreEqual(e1.MessageToBeSent, result.MessageToBeSent);
            Assert.AreEqual(e1.EndPoint, result.EndPoint);
            Envelope result2 = q.Dequeue(1);
            Assert.AreEqual(e2.MessageToBeSent, result2.MessageToBeSent);
            Assert.AreEqual(e2.EndPoint, result2.EndPoint);
            Envelope result3 = q.Dequeue(1);
            Assert.AreEqual(e3.MessageToBeSent, result3.MessageToBeSent);
            Assert.AreEqual(e3.EndPoint, result3.EndPoint);
        }
    }
}
