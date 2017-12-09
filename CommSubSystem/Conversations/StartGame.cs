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
    public class StartGame : Conversation
    {
        public int _GameId { get; set; }
        public StartGame()
        {
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.StartGame,
                TypeOfMessage.Ack
            };
        }

        public override Message CreateFirstMessage()
        {
            StartGameMsg msg = new StartGameMsg()
            {
                ConvId = ConvId,
                MsgId = MessageId.Create(),
                MessageType = TypeOfMessage.StartGame,
                GameId = _GameId
            };
            return msg;
        }

        public override void InitatorConversation(ref object context)
        {
            ReliableSend(CreateFirstMessage());
        }

        public override void ResponderConversation(ref object context)
        {
            Send(CreateAck());

            StartGameMsg msg = Message.Decode<StartGameMsg>(incomingMsg);
            context = incomingMsg.ToString();
        }
    }
}
