using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace CommSubSystem
{
    public class TCPClient
    {
        private TcpClient client;
        private NetworkStream stream;

        public void SetupConnection(int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
            TcpListener server = new TcpListener(ep);
            server.Start();
            client = server.AcceptTcpClient();
            stream = client.GetStream();
        }

        public void ConnectToServer(IPEndPoint server)
        {
            client = new TcpClient();
            client.Connect(server);
            stream = client.GetStream();
        }

        public void Send(byte[] envelope)
        {
            stream.Write(envelope, 0, envelope.Length);
        }
    
        //need to check for errors
        public byte[] Receive()
        {
            var buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
