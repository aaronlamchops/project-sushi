using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace CommunicationSubsystem
{
    public class ConversationInvoker
    {

        private Thread _worker;
        private bool _keepRunning;

        private readonly ConcurrentQueue<Conversation> _todoQueue = new ConcurrentQueue<Conversation>();
        private readonly AutoResetEvent _enqueueOccurred = new AutoResetEvent(false);


        public void Start()
        {
            _keepRunning = true;
            _worker = new Thread(Run);
            _worker.Start();
        }

        public void Stop()
        {
            _keepRunning = false;
        }

        private void Run()
        {
            while(_keepRunning)
            {
                
            }
        }
    }
}
