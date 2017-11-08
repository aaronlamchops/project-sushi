using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedObjects;

namespace CommunicationSubsystem
{
    public abstract class ResponderConversation : Conversation
    {
        public Envelope IncomingEnvelope { get; set; }

        protected override bool Initialize()
        {
            ConvId = IncomingEnvelope?.MessageToBeSent?.ConvId; //need to change convID to MessageId
            if(ConvId != null)
            {
                SetupConversationQueue();
                RemoteEndPoint = IncomingEnvelope?.EndPoint;
                State = PossibleState.Working;
            }
            else
            {
                Error = new Error() { Text = $"Cannot initialize {GetType().Name} conversation because ConvId is Incoming Message as null" };
            }

            return (Error == null);
        }
    }
}
