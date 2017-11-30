using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommSubSystem;
using Messages;
using SharedObjects;
using System.Net;

namespace CommunicationSubsystemTest
{
    [TestClass]
    public class UDPCommunicatorTester
    {
        //trust that 3rd party UDP send works because our class is
        //a singleton
        [TestMethod]
        public void UdpCommunicator_TestSetIP()
        {
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 0);
            UDPClient.UDPInstance.SetServerIP(ip);
            Assert.AreEqual(ip, UDPClient.UDPInstance.GetEndPoint());
            UDPClient.UDPInstance.SetServerIP("127.0.0.1", "5");
            IPEndPoint ip2 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5);
            Assert.AreEqual(ip2, UDPClient.UDPInstance.GetEndPoint());
        }


        [TestMethod]
        public void UdpCommunicator_TestTCPConnect()
        {
            TCPClient tc1 = new TCPClient();
            TCPClient tc2 = new TCPClient();

            IPEndPoint tc1Address = new IPEndPoint(IPAddress.Any, 1025);

            MessageId msgid = new MessageId() { Pid = 1, Seq = 1};
            Message msg = new Message()
            {
                MsgId = msgid,
                ConvId = msgid,
                MessageType = TypeOfMessage.Ack
            };

            tc1.SetupConnection(1025);
            tc2.ConnectToServer(tc1Address);

            tc1.Send(msg.Encode());
            Thread.Sleep(100);
            byte[] msgbytes = tc2.Receive();

            Assert.AreEqual(msg.Encode(), msgbytes);
        }
    }
}
