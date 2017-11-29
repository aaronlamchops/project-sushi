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
        public ConversationDictionary() { }

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

        public void SetupConversation(MessageId convId, Envelope env)
        {
            ConversationQueue queue;
            queue = Lookup(convId);

            if (queue == null)
            {
                queue = CreateQueue(convId);
            }
            queue.Enqueue(env);
        }

    }
}
