using System;
namespace CommunicationSubsystem
{
    public class ConversationFactory
    {
        //This `_SingleInstance` is the ONLY instance of a ConversationFactory
        private static ConversationFactory _SingleInstance;

        //A lock so that this class is thread safe
        private static readonly object MyLock = new object();

        //Private constructor to help further the idea of a Singleton ConversationFactory
        private ConversationFactory() { }


        /*
         * A Singleton is a class that is super unique in the fact that there is ONLY ONE instance of this class
         * A Singleton prevents other classes from making multiple instances of this classs.
         * This is super helpful in making our conversation creation Thread Safe and that we aren't creating multiple copies
         */


        //Method to access the ConversationFactory 
        public static ConversationFactory SingleInstance
        {
            get
            {
                lock (MyLock)
                {
                    if(_SingleInstance == null)
                    {
                        _SingleInstance = new ConversationFactory();
                    }
                }
                return _SingleInstance;
            }
        }

        //creates a conversation with an envelope parameter
        public void CreateConversation(Envelope envelope)
        {
            


        }
    }
}
