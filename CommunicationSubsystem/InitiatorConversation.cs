using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using SharedObjects;

namespace CommunicationSubsystem
{
    public abstract class InitiatorConversation : Conversation
    {
        protected Envelope FirstEnvelope { get; set; }

        protected override bool Initialize()
        {
            FirstEnvelope = null;

            ConvId = MessageId.Create();
            SetupConversationQueue();
            State = PossibleState.Working;

            Message msg = CreateFirstMessage();
            if(msg != null)
            {
                //msg.SetMessageAndConversationIds(ConvId); // need to either implement this function or se the ids manually
                FirstEnvelope = new Envelope() { MessageToBeSent = msg, EndPoint = RemoteEndPoint };
            }

            return (FirstEnvelope != null);

        }

        protected abstract Message CreateFirstMessage();

    }
}
