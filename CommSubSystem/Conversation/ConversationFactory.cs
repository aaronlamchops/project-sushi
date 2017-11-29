using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedObjects;
using System.Net;

namespace CommSubSystem.Conversation
{
    class ConversationFactory
    {
        private static ConversationFactory _Instance;
        private static readonly object MyLock = new object();
        private ConversationFactory() { }
        public int DefaultTimeout = 300;
        public int DefaultMaxRetries = 3;
        private ConversationDictionary ConversationDict = new ConversationDictionary();


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

        public Conversation CreateFromMessage(Envelope env)
        {
            Conversation conversation = null;
            Type convType = MatchMessageTypeToConversation(env.MessageTypeInEnvelope);

            if (convType != null)
            {
                bool initiator = false;
                MessageId ConvId = env.MessageToBeSent.ConvId;
                ConversationQueue queue = ConversationDict.CreateQueue(ConvId);

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
                }
            }

            return conversation;
        }

        public T CreateFromConversationType<T>(IPEndPoint receiver) where T : Conversation, new()
        {
           bool initiator = true;
           MessageId ConvId = MessageId.Create();
           ConversationQueue queue = ConversationDict.CreateQueue(ConvId);

            T conversation = new T()
            {
                ConvId = ConvId,
                Timeout = DefaultTimeout,
                MaxRetries = DefaultMaxRetries,
                Done = false,
                InitiatorConv = initiator,
                EndIP = receiver,
                MyQueue = queue
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
