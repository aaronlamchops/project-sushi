using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;
using System.Net;

namespace CommSubSystem.Conversation
{
    public abstract class Conversation
    {
        public MessageId ConvId { get; set; }
        public int Timeout { get; set; }
        public int MaxRetries { get; set; }
        public bool Done { get; set; }
        public bool InitiatorConv { get; set; }
        public IPEndPoint EndIP { get; set; }
        public Error Error { get; set; }

        public ConversationQueue MyQueue { get; set; }
        
        public void Initialize()
        {
            if (InitiatorConv)
            {
                SendFirstMessage();
                InitatorConversation();
            }
            else
            {
                ResponderConversation();
            }

            Done = true;
        }

        protected void ReliableSend(Envelope env)
        {
            Envelope incomingEnvelope = null;
            int remainingSends = MaxRetries;
            while(remainingSends > 0 && incomingEnvelope == null)
            {
                byte[] bytes = env.Encode();
                UDPClient.UDPInstance.SetServerIP(EndIP);
                UDPClient.UDPInstance.Send(bytes);

                remainingSends--;

                if (Error != null) break;

                incomingEnvelope = MyQueue.Dequeue(Timeout);
            }
        }


        public abstract void ResponderConversation();
        public abstract void InitatorConversation();
        public abstract void SendFirstMessage();
    }
}
