using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;

namespace CommSubSystem.ConversationClass
{
    public class CreateGameConv : Conversation
    {
        //for initiator
        public string _GameName { get; set; }
        public int _MinPlayers{ get; set;}
        public int _MaxPlayers{ get; set;}
        //for response
        public int _GameId { get; set; }

        public override void ResponderConversation(object context)
        {
            CreateGameReply msg = new CreateGameReply() { GameId = _GameId };
            msg.ConvId = ConvId;
            msg.MsgId = MessageId.Create();

            Envelope env = new Envelope()
            {
                EndPoint = UDPClient.UDPInstance.GetPublicEndPoint(),
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.CreateGameReply
            };

            ReliableSend(env);
        }

        public override void InitatorConversation(object context)
        {
            Envelope env = CreateFirstMessage();
            ReliableSend(env);

            //can parse message received
            CreateGameReply msg = incomingEnvelope.MessageToBeSent as CreateGameReply;
            //whatever logic will help in the post action

            Send(CreateAwk());
        }

        public override Envelope CreateFirstMessage()
        {
            CreateGame msg = new CreateGame() { MinPlayers = _MinPlayers, MaxPlayers = _MaxPlayers };
            msg.ConvId = ConvId;
            msg.MsgId = ConvId;


            Envelope env = new Envelope()
            {
                EndPoint = UDPClient.UDPInstance.GetPublicEndPoint(),
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.CreateGame
            };
            return env;
        }
    }
}
