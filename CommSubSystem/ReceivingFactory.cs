using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using SharedObjects;
using System.Net;
using Messages;

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
            IPEndPoint remoteEp;
            while (_keepReceiving)
            {
                remoteEp = new IPEndPoint(IPAddress.Any, 0);
                bytes = UDPClient.UDPInstance.Receive(ref remoteEp);

                if (bytes != null && bytes.Length > 0)
                {
                    RespondToMessage(bytes, remoteEp);
                    bytes = null;
                }

                TCPReceive();
            }
        }

        public abstract void TCPReceive();

        public void RespondToMessage(byte[] bytes, IPEndPoint remoteEp)
        {
            Message msg = Message.Decode<Message>(bytes);
            MessageId convId = msg.ConvId;
            ConversationQueue queue = ConversationDictionary.Instance.Lookup(convId);
            if (queue == null)
            {
                ExecuteBasedOnType(bytes, msg.MessageType, remoteEp);
            }
            else
            {
                queue.Enqueue(bytes);
            }
        }

        protected abstract void ExecuteBasedOnType(byte[] bytes, TypeOfMessage type, IPEndPoint refEp);

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
    }
}
