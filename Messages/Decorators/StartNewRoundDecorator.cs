using System;
namespace Messages.Decorators
{
    public class StartNewRoundDecorator : MessageDecorator
    {
        /*
         * This class can implement its own member variables 
         * or override existing member variables if needed
         */

        //not quite sure what to do for Link List?


        //TCP


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
