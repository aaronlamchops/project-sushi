using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;

namespace CommSubSystem.ConversationClass
{
    public class JoinGameConv : Conversation
    {
        public int _GameId { get; set; }
        public Player _Player { get; set; }
        public Game _Game { get; set; }

        public JoinGameConv()
        {
            allowedMessageTypes = new List<TypeOfMessage>
            {
                TypeOfMessage.JoinGame,
                TypeOfMessage.JoinGameReply,
                TypeOfMessage.Ack
            };
        }

        public override void ResponderConversation(ref object context)
        {
            JoinGameReply msg = new JoinGameReply()
            {
                Game = _Game,
                ConvId = ConvId,
                MsgId = MessageId.Create(),
                MessageType = TypeOfMessage.CreateGameReply
            };

            ReliableSend(msg);
        }

        public override void InitatorConversation(ref object context)
        {
            Message msg = CreateFirstMessage();
            ReliableSend(msg);

            if (Error != null) return;

            JoinGameReply reply = Message.Decode<JoinGameReply>(incomingMsg);
            _Game = reply.Game;
            //do something if needed with context;
            context = _Game;

            Send(CreateAck());
        }

        public override Message CreateFirstMessage()
        {
            JoinGame msg = new JoinGame()
            {
                GameID = _GameId,
                Player = _Player,
                ConvId = ConvId,
                MsgId = ConvId,
                MessageType = TypeOfMessage.JoinGame
            };

            return msg;
        }
    }
}
