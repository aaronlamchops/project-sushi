using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;

namespace CommSubSystem.ConversationClass
{
    public class LobbyHeartbeatConv : Conversation
    {
        //for initiator
        public int _NumberOfPlayers { get; set; }
        //for response
        //none

        public override void ResponderConversation(object context)
        {
            Send(CreateAwk());
        }

        public override void InitatorConversation(object context)
        {
            Envelope env = CreateFirstMessage();
            ReliableSend(env);

            if (incomingEnvelope != null)
            {
                Send(CreateAwk());
            }
        }

        public override Envelope CreateFirstMessage()
        {
            LobbyHeartbeat msg = new LobbyHeartbeat() { NumberOfPlayers = _NumberOfPlayers };
            msg.ConvId = ConvId;
            msg.MsgId = ConvId;


            Envelope env = new Envelope()
            {
                EndPoint = UDPClient.UDPInstance.GetPublicEndPoint(),
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.LobbyHeartbeat
            };
            return env;
        }
    }
}
