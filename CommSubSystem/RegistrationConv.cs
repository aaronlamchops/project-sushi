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
        public short _processId { get; set; }
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
            RegistrationMsg msg = new RegistrationMsg()
            {
                MsgId = MessageId.Create(),
                ConvId = ConvId,
                MessageType = TypeOfMessage.RegistrationReply,
                Pid = _processId
            };
            Send(msg);
        }

        public override void InitatorConversation(object context)
        {
            Send(CreateFirstMessage());

            Receive();

            if (Error != null) return;

            RegistrationMsg msg = Message.Decode<RegistrationMsg>(incomingMsg);
            _processId = msg.Pid;
            LocalProcessInfo.Instance.ProcessId = msg.Pid;
        }

        //requests a processId
        public override Message CreateFirstMessage()
        {
            Message msg = new Message()
            {
                MessageType = TypeOfMessage.Registration,
                ConvId = ConvId,
                MsgId = MessageId.Create()
            };
            return msg;
        }
    }
}
