using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;
using System.Net;

namespace CommSubSystem.ConversationClass
{
    public class ConversationFactory
    {
        private static ConversationFactory _Instance;
        private static readonly object MyLock = new object();
        private ConversationFactory() { }
        public int DefaultTimeout = 30000;
        public int DefaultMaxRetries = 3;

        public static ConversationFactory Instance
        {
            get
            {
                lock (MyLock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new ConversationFactory();
                    }
                }
                return _Instance;
            }
        }

        public Conversation CreateFromMessage(Envelope env, Conversation.ActionHandler preAction, Conversation.ActionHandler postAction)
        {
            Conversation conversation = null;
            Type convType = MatchMessageTypeToConversation(env.MessageTypeInEnvelope);

            if (convType != null)
            {
                bool initiator = false;
                MessageId ConvId = env.MessageToBeSent.ConvId;
                ConversationQueue queue = ConversationDictionary.Instance.CreateQueue(ConvId);

                conversation = Activator.CreateInstance(convType) as Conversation;
                if(conversation!= null) { 
                    conversation.InitiatorConv = initiator;
                    conversation.ConvId = ConvId;
                    conversation.Timeout = DefaultTimeout;
                    conversation.MaxRetries = DefaultMaxRetries;
                    conversation.Done = false;
                    conversation.InitiatorConv = initiator;
                    conversation.EndIP = env.IpEndPoint;
                    conversation.MyQueue = queue;
                    conversation.PreExecuteAction = preAction;
                    conversation.PostExecuteAction = postAction;
                }
            }

            return conversation;
        }

        public T CreateFromConversationType<T>(IPEndPoint receiver, Conversation.ActionHandler preAction, Conversation.ActionHandler postAction) where T : Conversation, new()
        {
           bool initiator = true;
           MessageId ConvId = MessageId.Create();
           ConversationQueue queue = ConversationDictionary.Instance.CreateQueue(ConvId);

            T conversation = new T()
            {
                ConvId = ConvId,
                Timeout = DefaultTimeout,
                MaxRetries = DefaultMaxRetries,
                Done = false,
                InitiatorConv = initiator,
                EndIP = receiver,
                MyQueue = queue,
                PreExecuteAction = preAction,
                PostExecuteAction = postAction
           };
           return conversation;
        }

        private Type MatchMessageTypeToConversation(Envelope.TypeOfMessage msgType)
        {
            Type convType;
            switch (msgType)
            {
                case Envelope.TypeOfMessage.CreateGame:
                    convType = typeof(CreateGameConv);
                    break;
                
                default:
                    convType = null;
                    break;
            }
            return convType;
        }
    }
}
