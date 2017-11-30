using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Messages; // Class Library Messages
using SharedObjects;

namespace CommSubSystem
{
    [Serializable]
    public class Envelope
    {
        //Possible types of messages
        public enum TypeOfMessage
        {
            NotSet,//Type of message that isnt set yet when creating the Envelope
            CreateGame,
            CreateGameReply,
            ExitGame,
            HeartBeat,
            JoinGame,
            PassCard,
            SelectCard,
            StartGame,
            StartNewRound,
            UpdateChat,
            UpdateState,
            Ack,
            UserInfo,
            RequestGameList,
            SendGameList,
            RequestGameId,
            SendGameId,
            LobbyHeartbeat
        };

        //Lets us know what type of message is inside of the envelope
        public TypeOfMessage MessageTypeInEnvelope { get; set; } = TypeOfMessage.NotSet;

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

        public byte[] Encode()
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            formatter.Serialize(stream, this);
            return stream.ToArray();
        }
    }
}
