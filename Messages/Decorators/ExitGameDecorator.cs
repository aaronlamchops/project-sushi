using System;
namespace Messages.Decorators
{
    public class ExitGameDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */
        public int GameID { get; set; }
        public int PlayerID { get; set; }


        public override byte[] Encode()
        {
            Encoder buffer = new Encoder();
            buffer.Add(GameID);
            buffer.Add(PlayerID);
            return buffer.getBytes();
        }

        public override void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            GameID = buffer.readInt();
            PlayerID = buffer.readInt();
        }
    }
}
