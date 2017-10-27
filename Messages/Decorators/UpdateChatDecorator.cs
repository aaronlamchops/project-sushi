using System;
namespace Messages.Decorators
{
    public class UpdateChatDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */
        public string Message { get; set; }
        public int PlayerID { get; set; }


        public override byte[] Encode()
        {
            Encoder buffer = new Encoder();
            buffer.Add(Message);
            buffer.Add(PlayerID);
            return buffer.getBytes();
        }

        public override void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            Message = buffer.readString();
            PlayerID = buffer.readInt();
        }
    }
}
