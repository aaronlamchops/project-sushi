using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        private readonly Dictionary<int, Conversation> _ConversationDictionary = new Dictionary<int, Conversation>();

        //get the conversation you are looking for. 
        /*
         * Alittle fuzzy on if we need this method or to make this method better... This is why we might need to make it a Flyweight
         */
        public Conversation GetConversation(int id)
        {
            Conversation conv;

            if(_ConversationDictionary.ContainsKey(id))
            {
                conv = _ConversationDictionary[id];
            }
            else
            {
                conv = new Conversation();

            }
            return conv;
        }
    }
}
