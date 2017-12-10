using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommSubSystem.ConversationClass;
using Messages;
using SharedObjects;

namespace CommSubSystem.Conversations
{
    public class PassCards : Conversation
    {
        public List<CardTypes> _Hand { get; set; }
        public PassCards()
        {
            allowedMessageTypes = new List<TypeOfMessage>{
                TypeOfMessage.PassCard
            };
        }

        public override Message CreateFirstMessage()
        {
            PassCard msg = new PassCard()
            {
                ConvId = ConvId,
                Hand = _Hand,
                MsgId = ConvId
            };
            return msg;
        }

        public override void InitatorConversation(ref object context)
        {
            //TCP is automatically reliable - as long as heartbeat is working we can trust the send is reliable
            TCPSend(CreateFirstMessage());
        }

        public override void ResponderConversation(ref object context)
        {
            //TCP does not require ack for response to be reliable
        }
    }
}
