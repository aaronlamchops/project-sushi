using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommSubSystem.ConversationClass;
using Messages;

namespace CommSubSystem.Conversations
{
    public class ChooseCard : Conversation
    {
        int _PlayerId;
        SharedObjects.CardTypes _CardId;

        public override Message CreateFirstMessage()
        {
            SelectCard msg = new SelectCard()
            {
                PlayerID = _PlayerId,
                CardID = _CardId
            };
            return msg;
        }

        public override void InitatorConversation(ref object context)
        {
            TCPSend(CreateFirstMessage());
        }

        //no response required
        public override void ResponderConversation(ref object context)
        {
            throw new NotImplementedException();
        }
    }
}
