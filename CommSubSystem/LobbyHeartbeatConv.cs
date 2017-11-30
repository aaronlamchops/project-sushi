using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;
using System.Diagnostics;

namespace CommSubSystem.ConversationClass
{
    public class LobbyHeartbeatConv : Conversation
    {
        //for initiator
        public int _NumberOfPlayers { get; set; }
        //for response
        //none

        public LobbyHeartbeatConv(){
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.LobbyHeartbeat,
                TypeOfMessage.Ack
            };
        }

        public override void ResponderConversation(ref object context)
        {
            ReliableSend(CreateAck());
        }

        public override void InitatorConversation(ref object context)
        {
            Message msg = CreateFirstMessage();
            ReliableSend(msg);

            if (incomingMsg != null)
            {
                //Send(CreateAck());
                //Got Ack no need to time out
                Debug.WriteLine("Got ACK");
                
            }
            else{
                //Hearbeat timeout
            }

        }

        public override Message CreateFirstMessage()
        {
            LobbyHeartbeat msg = new LobbyHeartbeat()
            {
                NumberOfPlayers = _NumberOfPlayers,
                ConvId = ConvId,
                MsgId = ConvId,
                MessageType = TypeOfMessage.LobbyHeartbeat
            };
            
            return msg;
        }
    }
}
