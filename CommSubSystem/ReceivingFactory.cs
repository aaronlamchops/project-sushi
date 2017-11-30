using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using SharedObjects;
using System.Net;

namespace CommSubSystem
{
    public abstract class Receiver
    {
        private static Receiver _Instance;
        private static readonly object MyLock = new object();
        protected Receiver() { }

        //threading:
        private Thread _worker;
        protected bool _keepReceiving;

        public void Receiving()
        {
            byte[] bytes;
            Envelope env = null;
            IPEndPoint remoteEp;
            while (_keepReceiving)
            {
                remoteEp = new IPEndPoint(IPAddress.Any, 0);
                bytes = UDPClient.UDPInstance.Receive(ref remoteEp);
                env = Decipher(bytes);

                if (env != null)
                {
                    MessageId convId = env.MessageToBeSent.ConvId;
                    ConversationQueue queue = ConversationDictionary.Instance.Lookup(convId);
                    if (queue == null)
                    {
                        ExecuteBasedOnType(env, remoteEp);
                    }
                    else
                    {
                        queue.Enqueue(env);
                    }
                }
            }
        }

        protected abstract void ExecuteBasedOnType(Envelope env, IPEndPoint refEp);

        public void Start()
        {
            _keepReceiving = true;
            _worker = new Thread(Receiving);
            _worker.Start();
        }

        public void Stop()
        {
            _keepReceiving = false;
        }

        protected Envelope Decipher(byte[] bytes)
        {
            Envelope env = null;
            if (bytes != null)
            {
                env = UDPClient.Decode(bytes);
            }

            return env;
        }
    }
}
