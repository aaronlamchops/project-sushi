using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class MessageDecorator : Message
    {
        public Message DecoratedMessage { get; set; }
        public override int MsgId => DecoratedMessage.MsgId;
        public override int ConvId => DecoratedMessage.ConvId;

        /*
         * Implement more member variables if needed
         */

    }
}
