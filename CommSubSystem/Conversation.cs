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
        protected List<MessageId> MessageLog = new List<MessageId>();

        public delegate void ActionHandler(object context = null);
        public ActionHandler PreExecuteAction { get; set; }
        public ActionHandler PostExecuteAction { get; set; }

        public ConversationQueue MyQueue { get; set; }

        //main method allows for form actions before and after conversation
        public void Execute(object context = null)
        {
            PreExecuteAction?.Invoke(context);
            if (InitiatorConv)
            {
                Error = InitatorConversation(context);
            }
            else
            {
                Error = ResponderConversation(context);
            }
            if (Error == null)
            {
                PostExecuteAction?.Invoke(context);
            }
            Done = true;
            ConversationDictionary.Instance.CloseQueue(MyQueue.QueueID);
        }

        // Send with retries
        // if response comes in stores in incomingEnvelope
        protected void ReliableSend(Envelope env)
        {
            incomingEnvelope = null;
            int remainingSends = MaxRetries;
            while (remainingSends > 0 && incomingEnvelope == null)
            {
                byte[] bytes = env.Encode();
                UDPClient.UDPInstance.SetServerIP(EndIP);
                Error = UDPClient.UDPInstance.Send(bytes);

                remainingSends--;

                if (Error != null) break;

                incomingEnvelope = MyQueue.Dequeue(Timeout);

                if (!ValidateEnvelope(env))
                {
                    incomingEnvelope = null;
                }
            }
            //was not able to receive within timeout
            if (Error == null && incomingEnvelope == null)
            {
                Error = new Error()
                {
                    Text = $"Did not receive message"
                };
            }
        }

        // Checks for duplicates and good types
        protected bool ValidateEnvelope(Envelope env)
        {
            //received envelope
            if (incomingEnvelope != null)
            {
                //allowed type
                if (allowedMessageTypes.Contains(env.MessageTypeInEnvelope))
                {
                    //check for duplicate
                    if (!MessageLog.Contains(env.MessageToBeSent.MsgId))
                    {
                        //add Id to log for future checks
                        MessageLog.Add(env.MessageToBeSent.MsgId);
                        return true;
                    }
                }
            }
            return false;
        }

        //list of allowed messages
        protected List<Envelope.TypeOfMessage> allowedMessageTypes;

        // Sends once
        protected Error Send(Envelope env)
        {
            byte[] bytes = env.Encode();
            UDPClient.UDPInstance.SetServerIP(EndIP);
            return UDPClient.UDPInstance.Send(bytes);
        }

        //most reliable conversations require an ack so here's a basic one
        protected Envelope CreateAwk()
        {
            Ack msg = new Ack
            {
                ConvId = ConvId,
                MsgId = MessageId.Create()
            };

            Envelope env = new Envelope()
            {
                MessageToBeSent = msg,
                MessageTypeInEnvelope = Envelope.TypeOfMessage.Ack
            };
            return env;
        }

        //initial message for initiator
        public abstract Envelope CreateFirstMessage();
        //implement responce
        public abstract void ResponderConversation(object context);
        //implement send
        public abstract void InitatorConversation(object context);
    }
}
