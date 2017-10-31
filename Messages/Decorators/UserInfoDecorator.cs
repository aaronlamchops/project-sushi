using System;
namespace Messages.Decorators
{
    public class UserInfoDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */
        public string UserName { get; set; }


        /*public override byte[] Encode()
        {
            Encoder buffer = new Encoder();
            //add custom stuff
            return buffer.getBytes();
        }

        public override void Decode(byte[] message)
        {
            Decoder buffer = new Decoder(message);
            //custom stuff
        }*/
    }
}
