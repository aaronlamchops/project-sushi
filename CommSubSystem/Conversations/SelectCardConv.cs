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
    public class SelectCardConv : Conversation
    {
        public CardTypes _CardID { get; set; }
        public int _PlayerId { get; set; }
        public int _GameId { get; set; }

        public SelectCardConv()
        {
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.SelectCard
            };
        }

        public override Message CreateFirstMessage()
        {
            SelectCard msg = new SelectCard()
            {
                CardID = _CardID,
                PlayerID = _PlayerId,
                GameId = _GameId
            };

            return msg;
        }

        public override void InitatorConversation(ref object context)
        {
            TCPSend(CreateFirstMessage());
        }

        public override void ResponderConversation(ref object context)
        {
            
        }
    }
}
