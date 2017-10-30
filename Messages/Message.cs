using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    //Need messages for:
    /*
     * Create Game
     * Join Game
     * Start Game
     * Exit Game
     * Send User Info
     * Select Card - TCP?
     * Update Game State - TCP?
     * Update Chat
     * Pass Cards - TCP?
     * Start New Round - TCP?
     * Heartbeat
     */
     // Also need decorator
     // Tweak however works

    //Base-concrete message class
    public class Message{
        //creating send message
        Message(int msgId, int convId) {
            MsgId = msgId;
            ConvId = convId;
        }

        //received message
        Message(byte[] message)
        {
            Decode(message);
        }

        public byte[] byteMessage { get; set; }
        public virtual int MsgId { get; set; }
        public virtual int ConvId { get; set; }


        public virtual byte[] Encode()
        {
            Encoder buffer = new Encoder();
            buffer.Add(MsgId);
            buffer.Add(ConvId);
            return buffer.getBytes();
        }

        public virtual void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            MsgId = buffer.readInt();
            ConvId = buffer.readInt();
            byteMessage = message;
        }

        protected void SkipDecodeHeader(Decoder buffer)
        {
            buffer.readInt();
            buffer.readInt();
        }

        protected void EncodeHeader(Encoder buffer)
        {
            buffer.Add(MsgId);
            buffer.Add(ConvId);
        }
    }
}