using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharedObjects;

namespace Messages
{
    public class MessageDecorator : Message
    {
        public Message DecoratedMessage { get; set; }
        public override MessageId MsgId => DecoratedMessage.MsgId;
        public override MessageId ConvId => DecoratedMessage.ConvId;

        /*
         * Implement more member variables if needed
         */

    }
}
