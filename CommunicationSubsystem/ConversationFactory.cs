using System;
using System.Collections.Generic;

namespace CommunicationSubsystem
{
    public abstract class ConversationFactory
    {
        private readonly Dictionary<Type, Type> _typeMappings = new Dictionary<Type, Type>();

        public CommSubSystem ManagingSubSystem { get; set; }

        public int DefaultMaxRetries { get; set; }
        public int DefaultTimeout { get; set; }

        public Conversation.ActionHandler PreExecuteAciton { get; set; }
        public Conversation.ActionHandler PostExecuteAciton { get; set; }

        public bool IncomingMessageCanStartConversation(Type messageType)
        {
            return _typeMappings.ContainsKey(messageType);
        }

        public virtual Conversation CreateFromMessage(Envelope envelope)
        {
            Conversation conversation = null;
            Type messageType = envelope?.MessageToBeSent?.GetType();

            if(messageType != null && _typeMappings.ContainsKey(messageType))
            {
                conversation = CreateResponderConversation(_typeMappings[messageType], envelope);
            }

            return conversation;
        }

        public virtual T CreateFromConversationType<T>() where T : Conversation, new()
        {
            T conversation = new T()
            {
                CommSubSystem = ManagingSubSystem,
                MaxRetries = DefaultMaxRetries,
                Timeout = DefaultTimeout,
                PreExecuteAction = PreExecuteAciton,
                PostExecuteAction = PostExecuteAciton
            };
            return conversation;
        }

        public abstract void Initialize();

        protected void Add(Type messageType, Type converationType)
        {
            if (messageType != null && converationType != null && !_typeMappings.ContainsKey(messageType))
            {
                _typeMappings.Add(messageType, converationType);
            }
        }

        protected virtual ResponderConversation CreateResponderConversation(Type conversationType, Envelope envelope = null)
        {
            ResponderConversation conversation = null;

            if(conversation != null)
            {
                conversation = Activator.CreateInstance(conversationType) as ResponderConversation;
                if(conversation != null)
                {
                    conversation.CommSubSystem = ManagingSubSystem;
                    conversation.MaxRetries = DefaultMaxRetries;
                    conversation.Timeout = DefaultTimeout;
                    conversation.PreExecuteAction = PreExecuteAciton;
                    conversation.PostExecuteAction = PostExecuteAciton;
                }
            }
            return conversation;
        }
    }
}
