using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using SharedObjects;

namespace CommunicationSubsystem
{
    public abstract class InitiatorRRConversation : InitiatorConversation
    {
        protected override void ExecuteDetails(object context)
        {
            Envelope env = ReliableSend(FirstEnvelope, ExpectedReplyType, typeof(HeartbeatMessage));

            if(env == null)
            {
                Error = new Error() { Text = $"No Response received" };
            }
            else if(env.MessageToBeSent is HeartbeatMessage)
            {
                Error = ((HeartbeatMessage)env.MessageToBeSent).Error;
            }
            else{
                ProcessValidResponse(env);
            }
        }

        protected abstract Type ExpectedReplyType { get; }

        protected abstract void ProcessValidResponse(Envelope env);
    }
}
