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
    public class ConnectGameServer : Conversation
    {
        public int _GameId { get; set; }
        public int _NumPlayers { get; set; }
        public int _Port { get; set; }

        public ConnectGameServer()
        {
            allowedMessageTypes = new List<TypeOfMessage>()
            {
                TypeOfMessage.Ack,
                TypeOfMessage.ConnectGameServerMsg
            };
        }

        public override Message CreateFirstMessage()
        {
            ConnectGSMsg msg = new ConnectGSMsg()
            {
                ConvId = ConvId,
                MsgId = MessageId.Create(),
                MessageType = TypeOfMessage.ConnectGameServerMsg,
                GameId = _GameId,
                Players = _NumPlayers,
                Port = _Port
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
        }
    }
}
