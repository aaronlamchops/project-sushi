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

        public CreateGameConv()
        {
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.CreateGame,
                TypeOfMessage.CreateGameReply,
                TypeOfMessage.Ack
            };
        }

        public override void ResponderConversation(object context)
        {
            CreateGameReply msg = new CreateGameReply() {
                GameId = _GameId,
                ConvId = ConvId,
                MsgId = MessageId.Create(),
                MessageType = TypeOfMessage.CreateGameReply
            };

            ReliableSend(msg);
        }

        public override void InitatorConversation(object context)
        {
            Message msg = CreateFirstMessage();
            ReliableSend(msg);

            if (Error != null) return;
            
            //can parse message received
            CreateGameReply reply = Message.Decode<CreateGameReply>(incomingMsg);
            //whatever logic will help in the post action

            Send(CreateAwk());
        }

        public override Message CreateFirstMessage()
        {
            CreateGame msg = new CreateGame()
            {
                MinPlayers = _MinPlayers,
                MaxPlayers = _MaxPlayers,
                ConvId = ConvId,
                MsgId = ConvId,
                MessageType = TypeOfMessage.CreateGame
            };
            
            return msg;
        }
    }
}
