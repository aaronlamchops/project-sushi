using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;

namespace CommSubSystem.ConversationClass
{
    public class Registration : Conversation
    {
        public int _processId { get; set; }
        public Registration()
        {
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.Registration,
                TypeOfMessage.RegistrationReply
            };
        }

        public override void ResponderConversation(object context)
        {
            Message msg = new Message()
            {
                MsgId = MessageId.Create(),
                ConvId = ConvId,
                MessageType = TypeOfMessage.RegistrationReply
            };
            Send(msg);
        }

        public override void InitatorConversation(object context)
        {
            Send(CreateFirstMessage());
            if (Error != null) return;

            RegistrationMsg msg = Message.Decode<RegistrationMsg>(incomingMsg);
            _processId = msg.Pid;
            LocalProcessInfo.Instance.ProcessId = msg.Pid;
        }

        //requests a processId
        public override Message CreateFirstMessage()
        {
            Message env = new Message()
            {
                MessageType = TypeOfMessage.Registration
            };
            return env;
        }
    }
}
