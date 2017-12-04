using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace CommSubSystem
{
    public class TCPClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private SslStream sslStream;
        static X509Certificate serverCertificate = null;
        TcpListener server;

        public void SetupConnection(int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
            server = new TcpListener(ep);
            server.Start();
            Thread t = new Thread(Listening);
            t.Start();

        }

        public void Listening()
        {
            client = server.AcceptTcpClient();
            sslStream = new SslStream(
                client.GetStream(), false);
            // The certificate variable specifies the name of the file containing the machine certificate.
            //Should be placed in folder with the main .exe
            string certificate = "2048b-rsa-example.p12";
            serverCertificate = new X509Certificate2(certificate, "test");
            sslStream.AuthenticateAsServer(serverCertificate,
                    false, SslProtocols.Tls, true);
            sslStream.ReadTimeout = 5000;
            sslStream.WriteTimeout = 5000;
            Debug.WriteLine("Listen done");
        }

        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // return false to not allow this client to communicate with unauthenticated servers.
            //return false;

            // return true to use a self-signed certificate
            return true;
        }

        public void ConnectToServer(IPEndPoint server)
        {
            client = new TcpClient();
            client.Connect(server);
            sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
                );
            // The server name must match the name on the server certificate.
            string serverName = "";
            try
            {
                sslStream.AuthenticateAsClient(serverName);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Debug.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Debug.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                return;
            }
            sslStream.ReadTimeout = 500;
            sslStream.WriteTimeout = 5000;

        }

        public void Send(byte[] envelope)
        {
            sslStream.Write(envelope, 0, envelope.Length);
            sslStream.Flush();
        }
    
        public byte[] Receive()
        {
            var buffer = new byte[2048];
            MemoryStream ms = new MemoryStream();
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;

            do
            {
                try
                {
                    bytes = sslStream.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, bytes);
                    Decoder decoder = Encoding.UTF8.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                    decoder.GetChars(buffer, 0, bytes, chars, 0);
                    messageData.Append(chars);
                }
                catch
                {
                    break;//End of message
                }
        } while (bytes != 0);

            return ms.ToArray();
        }
    }
}
