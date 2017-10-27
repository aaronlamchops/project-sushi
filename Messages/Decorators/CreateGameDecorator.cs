using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class CreateGameDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }


        public override byte[] Encode()
        {
            Encoder buffer = new Encoder();
            buffer.Add(MinPlayers);
            buffer.Add(MaxPlayers);
            return buffer.getBytes();
        }

        public override void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            MinPlayers = buffer.readInt();
            MaxPlayers = buffer.readInt();
        }
    }
}
