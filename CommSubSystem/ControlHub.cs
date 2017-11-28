using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Messages;
using SharedObjects;

namespace CommSubSystem
{
    //this class will manage the ConversationDictionary
    public class ControlHub
    {
        private readonly ConversationDictionary _queueDictionary = new ConversationDictionary();
        public bool ForceRedraw { get; set; }
        private static readonly object MyLock = new object(); 

        //add conversations to the dictionary
        public ConversationQueue SetupConverationQueue(MessageId convId)
        {
            return _queueDictionary.CreateQueue(convId);
        }

        public ConversationQueue Lookup(MessageId convId)
        {
            return _queueDictionary.Lookup(convId);
        }

        //close/complete conversation
        public void CloseConversationQueue(MessageId convId)
        {
            _queueDictionary.CloseQueue(convId);
        }

        //clear everything
        public void ClearAllQueues()
        {
            _queueDictionary.ClearAllQueues();
        }

        public void SetupConversation(MessageId convId, Envelope env)
        {
            ConversationQueue queue;
            queue = _queueDictionary.Lookup(convId);

            if(queue != null)
            {
                queue.Enqueue(env);
            }
            else
            {
                _queueDictionary.CreateQueue(convId);
            }
        }

        public ListView UpdateListOfGames()
        {

            return new ListView();
        }
    }
}