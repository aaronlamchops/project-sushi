using System;
namespace Messages.Decorators
{
    public class HeartbeatDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */


        public override byte[] Encode()
        {
            Encoder buffer = new Encoder();
            //Need more info for class
            return buffer.getBytes();
        }

        public override void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            //custom decode
        }
    }
}
