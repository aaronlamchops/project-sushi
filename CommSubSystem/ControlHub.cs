using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using SharedObjects;

namespace CommSubSystem
{
    //this class will manage the ConversationDictionary
    public class ControlHub
    {
        private readonly ConversationDictionary _queueDictionary = new ConversationDictionary();


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


    }
}