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
            UserInfo
        };

        //Lets us know what type of message is inside of the envelope
        public TypeOfMessage MessageTypeInEnvelope { get; set; } = TypeOfMessage.NotSet;

        public Message MessageToBeSent { get; set; }

        public Envelope() { }

        public Envelope(Message msg)
        {
            MessageToBeSent = msg;
        }

        public byte[] Encode()
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();

            formatter.Serialize(stream, this);
            return stream.ToArray();
        }
    }
}
