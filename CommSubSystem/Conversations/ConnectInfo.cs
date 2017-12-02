using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommSubSystem.ConversationClass;
using Messages;
using System.Net;
using SharedObjects;

namespace CommSubSystem.Conversations
{
    public class ConnectInfo : Conversation
    {
        public IPEndPoint _gameServer;

        public ConnectInfo()
        {
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.ConnectInfoMsg,
                TypeOfMessage.Ack
            };
        }

        public override Message CreateFirstMessage()
        {
            ConnectMsg msg = new ConnectMsg()
            {
                ConvId = ConvId,
                MsgId = MessageId.Create(),
                MessageType = TypeOfMessage.ConnectInfoMsg,
                GameServer = _gameServer
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

            ConnectMsg msg = Message.Decode<ConnectMsg>(incomingMsg);
            context = msg.GameServer;
        }
    }
}
