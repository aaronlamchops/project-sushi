using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;

namespace CommSubSystem.ConversationClass
{
    public class RequestGameListConv : Conversation
    {
        public ConcurrentDictionary<int, Game> _LobbyGameList = new ConcurrentDictionary<int, Game>();

        public override Message CreateFirstMessage()
        {
            RequestGameList msg = new RequestGameList()
            {
                ConvId = ConvId,
                MsgId = ConvId,
                MessageType = TypeOfMessage.RequestGameList
            };
            return msg;
        }

        public override void InitatorConversation(object context)
        {
            Message msg = CreateFirstMessage();
            ReliableSend(msg);

            if (Error != null) return;

            RequestGameListReply reply = Message.Decode<RequestGameListReply>(incomingMsg);
            //_LobbyGameList = reply.LobbyGameList;

            context = _LobbyGameList;

            Send(CreateAck());
        }

        public override void ResponderConversation(object context)
        {
            RequestGameListReply msg = new RequestGameListReply()
            {
                LobbyGameList = _LobbyGameList,
                MessageType = TypeOfMessage.RequestGameListReply
            };


            ReliableSend(msg);
        }
    }
}
