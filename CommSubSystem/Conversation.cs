using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;
using System.Net;
using Messages;
using System.Threading;

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
        public byte[] incomingMsg;
        protected List<MessageId> MessageLog = new List<MessageId>();

        public delegate void ActionHandler(object context = null);
        public ActionHandler PreExecuteAction { get; set; }
        public ActionHandler PostExecuteAction { get; set; }
        public ActionHandler FailureAction { get; set; }

        public ConversationQueue MyQueue { get; set; }

        public void Start(object context = null)
        {
            Thread convThread = new Thread(Execute);
            convThread.Start();
        }

        //main method allows for form actions before and after conversation
        public void Execute(object context)
        {
            PreExecuteAction?.Invoke(context);
            if (InitiatorConv)
            {
                InitatorConversation(ref context);
            }
            else
            {
                ResponderConversation(ref context);
            }
            if (Error == null)
            {
                PostExecuteAction?.Invoke(context);
            }
            else
            {
                FailureAction?.Invoke(context);
            }
            Done = true;
            ConversationDictionary.Instance.CloseQueue(MyQueue.QueueID);
        }

        public void Receive()
        {
            incomingMsg = MyQueue.Dequeue(Timeout);
            if (incomingMsg == null)
            {
                Error = new Error()
                {
                    Text = $"Did not receive message"
                };
            }
        }

    // Send with retries
    // if response comes in stores in incomingEnvelope
    protected void ReliableSend(Message msg)
        {
            incomingMsg = null;
            int remainingSends = MaxRetries;
            while (remainingSends > 0 && incomingMsg == null)
            {
                byte[] bytes = msg.Encode();
                UDPClient.UDPInstance.SetServerIP(EndIP);
                Error = UDPClient.UDPInstance.Send(bytes);

                remainingSends--;

                if (Error != null) break;

                incomingMsg = MyQueue.Dequeue(Timeout);

                if (!ValidateEnvelope(msg))
                {
                    incomingMsg = null;
                }
            }
            //was not able to receive within timeout
            if (Error == null && incomingMsg == null)
            {
                Error = new Error()
                {
                    Text = $"Did not receive message"
                };
            }
        }

        // Checks for duplicates and good types
        public bool ValidateEnvelope(Message msg)
        {
            //received envelope
            if (incomingMsg != null)
            {
                //allowed type
                if (allowedMessageTypes.Contains(msg.MessageType))
                {
                    //check for duplicate
                    if (!MessageLog.Contains(msg.MsgId))
                    {
                        //add Id to log for future checks
                        MessageLog.Add(msg.MsgId);
                        return true;
                    }
                }
            }
            return false;
        }

        //list of allowed messages
        protected List<TypeOfMessage> allowedMessageTypes;

        // Sends once
        protected Error Send(Message msg)
        {
            byte[] bytes = msg.Encode();
            UDPClient.UDPInstance.SetServerIP(EndIP);
            return UDPClient.UDPInstance.Send(bytes);
        }

        //most reliable conversations require an ack so here's a basic one
        protected Ack CreateAck()
        {
            Ack msg = new Ack
            {
                ConvId = ConvId,
                MsgId = MessageId.Create(),
                MessageType = TypeOfMessage.Ack
            };
            return msg;
        }

        //initial message for initiator
        public abstract Message CreateFirstMessage();
        //implement responce
        public abstract void ResponderConversation(ref object context);
        //implement send
        public abstract void InitatorConversation(ref object context);
    }
}
