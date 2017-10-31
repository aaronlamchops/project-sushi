using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Messages
{
    [Serializable]
    public class Message{

        static Message(){}

        public virtual int MsgId { get; set; }
        public virtual int ConvId { get; set; }

        public byte[] Encode()
        {
            IFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            return stream.ToArray();
        }

        public static Message Decode(byte[] message)
        {
            Message result = null;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream(message);

            result = (Message) formatter.Deserialize(stream);

            return result;
        }

    }
}