using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


using SharedObjects;

namespace CommunicationSubsystem
{
    public class ConversationDictionary
    {
        /*
         * Might need to make this a Flyweight
         * We could combine this class into the Conversation Factory
         * 
         * Will probably need to map Conversations to certain ids 
         */

        //Holds all conversations in the sub system
        private readonly ConcurrentDictionary<MessageId, ConversationQueue> _ConversationDictionary = new ConcurrentDictionary<MessageId, ConversationQueue>(new MessageId.MessageIdComparer());

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
        
    }
}
