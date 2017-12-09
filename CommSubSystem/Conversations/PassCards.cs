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
            allowedMessageTypes = new List<TypeOfMessage>{ };
        }

        public override Message CreateFirstMessage()
        {
            PassCard msg = new PassCard()
            {
                Hand = _Hand
            };
            return msg;
        }

        public override void InitatorConversation(ref object context)
        {
            TCPSend(CreateFirstMessage);
        }

        public override void ResponderConversation(ref object context)
        {
            //TCP does not require response to be reliable
        }
    }
}
