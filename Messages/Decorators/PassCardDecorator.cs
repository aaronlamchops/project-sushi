using System;
using System.Collections.Generic;

namespace Messages.Decorators
{
    public class PassCardDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */

        //not quite sure what to do for Link List?
        public LinkedList<string> Hand { get; set; }

        //TCP


        /*public override byte[] Encode()
            {
                Encoder buffer = new Encoder();
                //Add needed variables
                return buffer.getBytes();
            }

            public override void Decode(byte[] message)
            {
                Decoder buffer = new Decoder(message);
                //custom decode
            }
        }*/
    }
}
