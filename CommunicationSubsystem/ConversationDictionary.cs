using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly Dictionary<MessageId, Conversation> _ConversationDictionary = new Dictionary<MessageId, Conversation>();

        
    }
}
