using System;
using System.Net;
using System.Net.Sockets;

using Messages; // Class Library Messages
using SharedObjects;

namespace CommunicationSubsystem
{

    //Implementation from Dr.Clydes exmaple code, using Message instead of ControlMessage
    public class Envelope
    {
        /*
         * Must contain IPEndPoints for incoming and outgoing messages
         */
        public Message MessageToBeSent { get; set; }

        public PublicEndPoint EndPoint { get; set; }

        public Envelope() { }

        public Envelope(Message msg, PublicEndPoint endPoint)
        {
            MessageToBeSent = msg;
            EndPoint = endPoint;
        }

        public Envelope(Message message, IPEndPoint ep) :
            this(message, (ep != null) ? new PublicEndPoint() { IpEndPoint = ep } : null) { }

        public IPEndPoint IpEndPoint
        {
            get
            {
                return (EndPoint == null) ?
                    new IPEndPoint(IPAddress.Any, 0) :
                    EndPoint.IpEndPoint;
            }
            set { EndPoint = (value == null) ? null : new PublicEndPoint() { IpEndPoint = value }; }
        }

        public bool IsValidToSend => (MessageToBeSent != null &&
                                      EndPoint != null &&
                                      EndPoint.Host != "0.0.0.0" &&
                                      EndPoint.Port != 0);

    }
}
