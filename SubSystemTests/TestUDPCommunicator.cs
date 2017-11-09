//using System;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using CommSubSystem;
//using Messages;
//using SharedObjects;

//namespace CommunicationSubsystemTest
//{
//    [TestClass]
//    public class UDPCommunicatorTester
//    {
//        private Envelope _lastIncomingEnvelope1;
//        private Envelope _lastIncomingEnvelope2;

//        [TestMethod]
//        public void UdpCommunicator_TestSendAndReceive()
//        {
//            LocalProcessInfo.Instance.ProcessId = 10;

//            UDPClient comm1 = new UDPClient()
//            {
//                MinPort = 10000,
//                MaxPort = 10999,
//                Timeout = 1000,
//                EnvelopeHandler = ProcessEnvelope1
//            };

//            comm1.Start();

//            UDPClient comm2 = new UDPClient()
//            {
//                MinPort = 10000,
//                MaxPort = 10999,
//                Timeout = 1000,
//                EnvelopeHandler = ProcessEnvelope2
//            };
//            comm2.Start();

//            PublicEndPoint targetEndPoint = new PublicEndPoint() { Host = "127.0.0.1", Port = comm2.Port };

//            ExitGame msg = new ExitGame() { MsgId = 1, ConvId = 1, PlayerID = 1, GameID = 1 };
//            Envelope env = new Envelope(msg, targetEndPoint);

//            comm1.Send(env);

//            Thread.Sleep(100);

//            Assert.IsNotNull(_lastIncomingEnvelope2);
//            Assert.IsNotNull(_lastIncomingEnvelope2.MessageToBeSent);
//            Assert.AreEqual(msg.MsgId, _lastIncomingEnvelope2.MessageToBeSent.MsgId);
//            Assert.AreEqual(msg.ConvId, _lastIncomingEnvelope2.MessageToBeSent.ConvId);
//            ExitGame msg2 = _lastIncomingEnvelope2.MessageToBeSent as ExitGame;
//            Assert.IsNotNull(msg2);
//        }

//        private void ProcessEnvelope1(Envelope env)
//        {
//            _lastIncomingEnvelope1 = env;
//        }

//        private void ProcessEnvelope2(Envelope env)
//        {
//            _lastIncomingEnvelope2 = env;
//        }

//    }
//}
