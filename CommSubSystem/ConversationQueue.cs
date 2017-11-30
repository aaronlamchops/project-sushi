using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using SharedObjects;

namespace CommSubSystem
{
    public class ConversationQueue
    {
        private readonly ConcurrentQueue<byte[]> _Queue = new ConcurrentQueue<byte[]>();    //queue to hold all concurrent and thread safe Envelopes
        private readonly ManualResetEvent _EnqueueOccurred = new ManualResetEvent(false);       //allows us to know when a conversation is pushed onto the queue and react

        public MessageId QueueID { get; set; }
        public int Count => _Queue.Count;

        public void Enqueue(byte[] envelope)      //Push something onto the queue and notify that something is enqueued
        {
            if(envelope != null)
            {
                _Queue.Enqueue(envelope);
                _EnqueueOccurred.Set();
            }
        }

        public byte[] Dequeue(int timeout)        //Dequeue an envelope read to be processed
        {
            byte[] result = null;
            int remainingTime = timeout;

            while(result == null && remainingTime > 0)
            {
                DateTime ts = DateTime.Now;

                if(_Queue.Count == 0)
                {
                    _EnqueueOccurred.WaitOne(timeout);
                }

                if(_Queue.TryDequeue(out result))
                {
                    _EnqueueOccurred.Reset();
                }
                else
                {
                    remainingTime -= Convert.ToInt32(DateTime.Now.Subtract(ts).TotalMilliseconds);
                }
            }

            return result;
        }
    }
}
