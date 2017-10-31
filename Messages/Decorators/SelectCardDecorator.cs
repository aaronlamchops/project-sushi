using System;
namespace Messages.Decorators
{
    public class SelectCardDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */
        public int CardID { get; set; }
        public int PlayerID { get; set; }


        //TCP


        /*public override byte[] Encode()
        {
            Encoder buffer = new Encoder();
            buffer.Add(CardID);
            buffer.Add(PlayerID);
            return buffer.getBytes();
        }

        public override void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            CardID = buffer.readInt();
            PlayerID = buffer.readInt();
        }*/
    }
}
