using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using SharedObjects;
using Messages;

namespace CommunicationSubsystem
{
    public abstract class Conversation
    {
        public enum PossibleState
        {
            NotInitialized,
            Working,
            Failed,
            Successed
        };

        public PossibleState State { get; protected set; } = PossibleState.NotInitialized;

        public CommSubSystem CommSubSystem { get; set; }
        public PublicEndPoint RemoteEndPoint { get; set; }
        public MessageId ConvId { get; protected set; }

        public Error Error { get; protected set; }

        public delegate void ActionHandler(object context);

        public ActionHandler PreExecuteAction { get; set; }
        public ActionHandler PostExecuteAction { get; set; }

        public int Timeout { get; set; }
        public int MaxRetries { get; set; }
        public bool Done { get; protected set; }

        public void Launch(object context = null)
        {
            bool result = ThreadPool.QueueUserWorkItem(Execute, context);
        }

        public virtual void Execute(object context = null)
        {
            PreExecuteAction?.Invoke(context);

            if(Initialize())
            {
                ExecuteDetails(context);
            }

            if(Error == null)
            {
                State = PossibleState.Successed;
            }
            else
            {
                State = PossibleState.Failed;
            }

            PostExecuteAction?.Invoke(context);

            if(UseConversationQueue)
            {
                CommSubSystem.CloseConversationQueue(ConvId);
            }
        }

        protected virtual bool UseConversationQueue => true;

        protected abstract bool Initialize();

        protected abstract void ExecuteDetails(object context);

        protected ConversationQueue MyQueue { get; set; }

        protected void SetupConversationQueue()
        {
            if(UseConversationQueue)
            {
                MyQueue = CommSubSystem.SetupConversationQueue(ConvId);
            }
            else
            {
                //log out error
            }
        }

        protected bool IsEnvelopeValid(Envelope env, params Type[] allowedTypes)
        {
            Error = null;

            if(env?.MessageToBeSent == null)
            {
                Error = new Error() { Text = "Null or empty message" };
            }
            else if(env.MessageToBeSent.MsgId == null)
            {
                Error = new Error() { Text = "Message Number" };
            }
            else if(env.MessageToBeSent.ConvId == null)
            {
                Error = new Error() { Text = "Null Conversation id" };
            }
            else
            {
                Type messageType = env.MessageToBeSent.GetType();

                if(!allowedTypes.Contains(messageType))
                {
                    Error = new Error() { Text = "Invalid Type of message. Allowed Types: " + allowedTypes.Aggregate(string.Empty, (current, t) => current + t.ToString()) };
                }
            }

            if(Error != null)
            {
                //log out error text
            }

            return (Error == null);
        }

        protected Envelope ReliableSend(Envelope outgoingEnv, params Type[] allowedTypes)
        {
            Envelope incomingEnvelope = null;

            int remainingSends = MaxRetries;
            while(remainingSends > 0 && incomingEnvelope == null)
            {
                Error = CommSubSystem.Send(outgoingEnv);
                remainingSends--;

                if(Error != null)
                {
                    break;
                }

                incomingEnvelope = MyQueue.Dequeue(Timeout);
                if(!IsEnvelopeValid(incomingEnvelope, allowedTypes))
                {
                    incomingEnvelope = null;
                }
            }

            if(Error != null)
            {
                //log out Error code
            }

            return incomingEnvelope;
        }
    }
}
