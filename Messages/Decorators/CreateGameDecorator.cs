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
            /*
             * This class can implement its own version of encode if needed
             */
            return null;
        }

        public override void Decode()
        {
            /*
             * This class can implement its own version of decode if needed
             */
        }
    }
}
