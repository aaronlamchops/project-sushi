using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace CommunicationSubsystem
{
    public class Conversation
    {
        public int ConversationID { get; set; }
        public Envelope FirstEnv { get; set; }
    }
}
