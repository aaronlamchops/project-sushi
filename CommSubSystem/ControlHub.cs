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

        //close/complete conversation
        public void CloseConversationQueue(MessageId convId)
        {
            _queueDictionary.CloseQueue(convId);
        }


    }
}
