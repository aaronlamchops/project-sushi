using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using SharedObjects;

namespace CommSubSystem.Conversation
{
    public class CreateGameConv : Conversation
    {
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public string GameName { get; set; }

        public override void ResponderConversation(object context)
        {

        }

        public override void InitatorConversation(object context)
        {
            //create a message out of this
            CreateGame msg = new CreateGame() { MinPlayers = MinPlayers, MaxPlayers = MaxPlayers, GameName = GameName };
            msg.ConvId = MessageId.Create();
            msg.MsgId = ConvId;

            Envelope env = new Envelope(msg, UDPClient.UDPInstance.GetEndPoint());
            env.MessageTypeInEnvelope = Envelope.TypeOfMessage.CreateGame;

            ReliableSend(env);
        }
    }
}
