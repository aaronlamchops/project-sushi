using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

using Messages;
using SharedObjects;


namespace CommunicationSubsystem
{
    public delegate void IncomingEnvelopeHandler(Envelope env);

    public class UDPCommunicator
    {
        private UdpClient _myUdpClient;
        private Thread _receiveThread;
        private bool _started;
        private static readonly object StartStopLock = new object();

        public int MinPort { get; set; }
        public int MaxPort { get; set; }
        public int Timeout { get; set; }
        public int Port => ((IPEndPoint)_myUdpClient?.Client.LocalEndPoint)?.Port ?? 0;
        public IncomingEnvelopeHandler EnvelopeHandler { get; set; }

        public void Start()
        {
            ValidPorts();

            lock (StartStopLock)
            {
                if (_started) Stop();

                int portToTry = FindAvailablePort(MinPort, MaxPort);

                if(portToTry > 0)
                {
                    try
                    {
                        IPEndPoint localEp = new IPEndPoint(IPAddress.Any, portToTry);
                        _myUdpClient = new UdpClient(localEp);
                        _started = true;
                    }
                    catch(SocketException)
                    {

                    }
                }

                if(!_started)
                {
                    throw new ApplicationException($"Cannot bind the socket to a port {portToTry}");
                }
                else
                {
                    _receiveThread = new Thread(Receive);
                    _receiveThread.Start();
                }
            }
        }

        public void Stop()
        {
            lock(StartStopLock)
            {
                _started = false;
                _receiveThread?.Join(Timeout * 2);
                _receiveThread = null;

                if(_myUdpClient != null)
                {
                    _myUdpClient.Close();
                    _myUdpClient = null;
                }
            }
        }

        public Error Send(Envelope outgoingEnvelope)
        {
            Error error = null;

            if(outgoingEnvelope == null || !outgoingEnvelope.IsValidToSend)
            {
                //warning invalid envelope or message
            }
            else
            {
                byte[] bytesToSend = outgoingEnvelope.MessageToBeSent.Encode();

                try
                {
                    _myUdpClient.Send(bytesToSend, bytesToSend.Length, outgoingEnvelope.EndPoint.IpEndPoint);
                }
                catch (Exception err)
                {
                    error = new Error()
                    {
                        Text = $"Cannot send a {outgoingEnvelope.MessageToBeSent.GetType().Name} to {outgoingEnvelope.EndPoint}: {err.Message}"
                    };
                }
            }
            return error;
        }

        public Error DropMulticastGroup(IPAddress groupAddress)
        {
            Error error = null;
            try
            {
                _myUdpClient.DropMulticastGroup(groupAddress);
            }
            catch(Exception err)
            {
                error = new Error() { Text = $"Cannot join multicast group: {err}" };
            }
            return error;
        }

        private void Receive()
        {
            while(_started)
            {
                Envelope env = ReceiveOne();
                if(env != null)
                {
                    EnvelopeHandler?.Invoke(env);
                }
            }
        }

        private Envelope ReceiveOne()
        {
            Envelope result = null;

            IPEndPoint ep;

            byte[] receivedBytes = ReceiveBytes(Timeout, out ep);
            if(receivedBytes != null && receivedBytes.Length > 0)
            {
                PublicEndPoint sendersEndPoint = new PublicEndPoint() { IpEndPoint = ep };
                Message message = Message.Decode(receivedBytes);                            //method Decode needs to be Static
                if(message != null)
                {
                    result = new Envelope(message, sendersEndPoint);

                }
                else
                {
                    //log out message or exception
                }
            }

            return result;
        }

        public byte[] ReceiveBytes(int timeout, out IPEndPoint ep)
        {
            byte[] receivedBytes = null;
            ep = null;

            if(_myUdpClient != null)
            {
                _myUdpClient.Client.ReceiveTimeout = timeout;
                ep = new IPEndPoint(IPAddress.Any, 0);
                
                try
                {
                    receivedBytes = _myUdpClient.Receive(ref ep);
                }
                catch(SocketException err)
                {

                }
                catch(Exception err)
                {

                }
            }

            return receivedBytes;
        }

        private void ValidPorts()
        {
            if((MinPort != 0 && (MinPort < IPEndPoint.MinPort || MinPort > IPEndPoint.MaxPort)) || 
               (MaxPort != 0 && (MaxPort < IPEndPoint.MinPort || MaxPort > IPEndPoint.MaxPort)))
            {
                throw new ApplicationException("Invalid port specifications");
            }
        }

        private int FindAvailablePort(int minPort, int maxPort)
        {
            int availablePort = -1;

            for (int possiblePort = minPort; possiblePort <= maxPort; possiblePort++)
            {
                if(!IsUsed(possiblePort))
                {
                    availablePort = possiblePort;
                    break;
                }
            }
            return availablePort;
        }

        private bool IsUsed(int port)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveUdpListeners();
            return endPoints.Any(ep => ep.Port == port);
        }
    }


    /*
     * Needs to be able to Create a Envelope
     * Needs to be able to Create a Message as well to be put into an Envelope
     */

    /*
     * needs a public Send Method that takes an Envelope as a parameter
     */


}
