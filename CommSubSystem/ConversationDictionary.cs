using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


using SharedObjects;

namespace CommSubSystem
{
    public class ConversationDictionary
    {
        private readonly ConcurrentDictionary<MessageId, ConversationQueue> _ConversationDictionary = new ConcurrentDictionary<MessageId, ConversationQueue>(new MessageId.MessageIdComparer());
        private static ConversationDictionary _Instance;
        private static readonly object MyLock = new object();
        private ConversationDictionary() { }

        public static ConversationDictionary Instance
        {
            get
            {
                lock (MyLock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new ConversationDictionary();
                    }
                }
                return _Instance;
            }
        }

        public ConversationQueue CreateQueue(MessageId convId)
        {
            ConversationQueue result = null;

            if(convId != null)
            {
                result = Lookup(convId);
                if (result == null)
                {
                    result = new ConversationQueue() { QueueID = convId };
                    _ConversationDictionary.TryAdd(convId, result);
                }
            }

            return result;
        }

        public ConversationQueue Lookup(MessageId convId)
        {
            ConversationQueue result;
            _ConversationDictionary.TryGetValue(convId, out result);

            return result;
        }

        public void CloseQueue(MessageId queueId)
        {
            ConversationQueue queue;
            _ConversationDictionary.TryRemove(queueId, out queue);
        }

        public void ClearAllQueues()
        {
            _ConversationDictionary.Clear();
        }

        public int ConversationQueueCount => _ConversationDictionary.Count;

        public ConversationQueue SetupConversation(MessageId convId, byte[] bytes)
        {
            ConversationQueue queue;
            queue = Lookup(convId);

            if (queue == null)
            {
                queue = CreateQueue(convId);
            }
            queue.Enqueue(bytes);
            return queue;
        }

    }
}
