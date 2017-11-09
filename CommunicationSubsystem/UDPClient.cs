using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Messages;

namespace CommunicationSubsystem
{
    class UDPClient
    {
        private readonly UdpClient _udpClient;
        public IPEndPoint _serverIp;

        public UDPClient()
        {
            IPEndPoint myIP = new IPEndPoint(IPAddress.Any, 0);
            _udpClient = new UdpClient(myIP);
            _udpClient.Client.ReceiveTimeout = 1000;
        }

        public byte[] Receive()
        {
            IPEndPoint remoteEp = new IPEndPoint(IPAddress.Any, 0);
            byte[] receiveBuffer = null;
            try
            {
                receiveBuffer = _udpClient.Receive(ref remoteEp);
            }
            catch { }
            return receiveBuffer;
        }
        
        public void Send(byte[] envelope)
        {
            _udpClient.Send(envelope, envelope.Length, _serverIp);
        }


        //We'll want to change this to allow for multiple servers and multicasting
        public void SetServerIP(string Address, string port)
        {
            _serverIp = new IPEndPoint(IPAddress.Parse(Address), Convert.ToInt32(port));
        }

        public void SetServerIP(IPEndPoint serverIP)
        {
            _serverIp = serverIP;
        }
    }

    class CommunicationStuff
    {
        public CommunicationStuff() {
            _myUdpClient = new UDPClient();
        }

        UDPClient _myUdpClient;
        //can have a list of conversations if nesacerry

        public void Send(Message message) //include MessageType in params
        {
            //pack into envelope
            //envelope should decode itself
            //send through client
        }

        public void Receive()
        {
            //some thread should be running and always receiving
            DoStuffWithMessage();

        }

        private void DoStuffWithMessage(Message message, messageType type)
        {
            //Conversation Rules go here
        }
    }
}
