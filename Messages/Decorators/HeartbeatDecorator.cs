﻿using System;
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