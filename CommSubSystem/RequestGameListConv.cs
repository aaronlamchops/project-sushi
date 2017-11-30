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

        public override Envelope CreateFirstMessage()
        {
            RequestGameList msg = new RequestGameList();
            msg.ConvId = ConvId;
            msg.MsgId = ConvId;

            Envelope env = new Envelope()
            {
                EndPoint = UDPClient.UDPInstance.GetPublicEndPoint(),
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.RequestGameList
            };
            return env;
        }

        public override void InitatorConversation(object context)
        {
            Envelope env = CreateFirstMessage();
            ReliableSend(env);

            if(incomingEnvelope != null)
            {
                RequestGameList msg = incomingEnvelope.MessageToBeSent as RequestGameList;

                Send(CreateAwk());
            }
        }

        public override void ResponderConversation(object context)
        {
            SendGameList msg = new SendGameList() { gameList = _LobbyGameList };
            msg.ConvId = ConvId;
            msg.MsgId = MessageId.Create();

            Envelope env = new Envelope()
            {
                EndPoint = UDPClient.UDPInstance.GetPublicEndPoint(),
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.SendGameList
            };

            ReliableSend(env);
        }
    }
}
