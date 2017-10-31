using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace CommunicationSubsystem
{
    public class ConversationInvoker
    {

        private Thread _worker;
        private bool _keepGoing;

        private readonly ConcurrentQueue<Envelope> _todoQueue = new ConcurrentQueue<Envelope>();    //Envelope Queue
        private readonly AutoResetEvent _enqueueOccurred = new AutoResetEvent(false);


        public void Start()
        {
            _keepGoing = true;
            _worker = new Thread(Run);
            _worker.Start();
        }

        public void Stop()
        {
            _keepGoing = false;
        }

        public void EnqueueEnvelopeForExecution(Envelope envelope)
        {
            if (envelope != null)
            {
                _todoQueue.Enqueue(envelope);
                _enqueueOccurred.Set();
            }
        }

        public void Run()
        {
            while (_keepGoing)
            {
                Envelope env;
                if(_todoQueue.TryDequeue(out env))
                {
                    //need implementation of Initiator Conversation
                    //and
                    //Responder Conversation

                    //we need to decipher whether the message is out-going or incoming
                    //then determine what to do
                }
            }
        }
    }
}
