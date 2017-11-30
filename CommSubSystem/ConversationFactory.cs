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
    public class ConversationFactory
    {
        private static ConversationFactory _Instance;
        private static readonly object MyLock = new object();
        private ConversationFactory() { }
        public int DefaultTimeout = 300;
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

        public T CreateFromMessage<T>(byte[] bytes, IPEndPoint refEp, Conversation.ActionHandler preAction, Conversation.ActionHandler postAction) where T : Conversation, new()
        {
            bool initiator = false;
            MessageId ConvId = Message.Decode<Message>(bytes).ConvId;
            ConversationQueue queue = ConversationDictionary.Instance.CreateQueue(ConvId);

            T conversation = new T()
            {
                InitiatorConv = initiator,
                ConvId = ConvId,
                Timeout = DefaultTimeout,
                MaxRetries = DefaultMaxRetries,
                Done = false,
                EndIP = refEp,
                MyQueue = queue,
                PreExecuteAction = preAction,
                PostExecuteAction = postAction
            };
            

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

        
    }
}
