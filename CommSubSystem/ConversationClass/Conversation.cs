using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;
using System.Net;
using Messages;

namespace CommSubSystem.ConversationClass
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
        protected Envelope incomingEnvelope;

        public delegate void ActionHandler(object context = null);
        public ActionHandler PreExecuteAction { get; set; }
        public ActionHandler PostExecuteAction { get; set; }


        public ConversationQueue MyQueue { get; set; }
        
        public void Execute(object context = null)
        {
            PreExecuteAction?.Invoke(context);
            if (InitiatorConv)
            {
                InitatorConversation(context);
            }
            else
            {
                ResponderConversation(context);
            }
            PostExecuteAction?.Invoke(context);
            Done = true;
            ConversationDictionary.Instance.CloseQueue(MyQueue.QueueID);
        }

        protected void ReliableSend(Envelope env)
        {
            incomingEnvelope = null;
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
        
        protected void Send(Envelope env)
        {
            byte[] bytes = env.Encode();
            UDPClient.UDPInstance.SetServerIP(EndIP);
        }

        protected Envelope CreateAwk()
        {
            Ack msg = new Ack();
            msg.ConvId = ConvId;
            msg.MsgId = MessageId.Create();

            Envelope env = new Envelope()
            {
                EndPoint = UDPClient.UDPInstance.GetPublicEndPoint(),
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.Ack
            };
            return env;
        }

        public abstract Envelope CreateFirstMessage();
        public abstract void ResponderConversation(object context);
        public abstract void InitatorConversation(object context);
    }
}
