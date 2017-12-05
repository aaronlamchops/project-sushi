using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using SharedObjects;

namespace Messages
{
    [Serializable]
    public class Message{

        static Message(){}

        public MessageId MsgId { get; set; }
        public MessageId ConvId { get; set; }
        //Lets us know what type of message is inside of the envelope
        public TypeOfMessage MessageType { get; set; } = TypeOfMessage.NotSet;

        public byte[] Encode()
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            return stream.ToArray();
        }

        public static T Decode<T>(byte[] message) where T : Message, new()
        {
            T result = null;

            if (message != null)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new MemoryStream(message);

                result = (T)formatter.Deserialize(stream);
            }
            return result;
        }

    }

    //Possible types of messages
    public enum TypeOfMessage
    {
        NotSet,//Type of message that isnt set yet when creating the Envelope
        CreateGame,
        CreateGameReply,
        ExitGame,
        HeartBeat,
        JoinGame,
        JoinGameReply,
        PassCard,
        SelectCard,
        StartGame,
        StartNewRound,
        UpdateChat,
        UpdateState,
        Ack,
        UserInfo,
        Registration,
        RegistrationReply,
        LobbyHeartbeat,
        RequestGameList,
        RequestGameListReply,
        ConnectInfoMsg,
        ConnectGameServerMsg
    };
}