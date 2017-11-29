using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

using Messages;
using SharedObjects;
using CommSubSystem.Commands;
using CommSubSystem.ConversationClass;

namespace CommSubSystem.Receive
{
    public class ReceivingFactory
    {
        private static ReceivingFactory _Instance;
        private static readonly object MyLock = new object();
        private ReceivingFactory() { }

        //threading:
        private Thread _worker;
        private bool _keepReceiving;

        public static ReceivingFactory Instance
        {
            get 
            {
                lock(MyLock)
                {
                    if(_Instance == null)
                    {
                        _Instance = new ReceivingFactory();
                    }
                }
                return _Instance;
            }
        }
        
        public ReceiveInvoker ReceiveInvoker { get; set; }

        public void Start()
        {
            _keepReceiving = true;
            _worker = new Thread(Receive);
            _worker.Start();
        }

        public void Stop()
        {
            _keepReceiving = false;
        }

        private void Receive()
        {
            byte[] bytes;
            Envelope env = null;
            while(_keepReceiving)
            {
                bytes = UDPClient.UDPInstance.Receive();
                env = Decipher(bytes);

                if(env != null)
                {
                    MessageId convId = env.MessageToBeSent.ConvId;
                    ConversationQueue queue = ConversationDictionary.Instance.Lookup(convId);
                    if(queue == null)
                    {
                        ExecuteBasedOnType(env);
                    }
                    else
                    {
                        queue.Enqueue(env);
                    }
                }
            }
        }

        public Conversation.ActionHandler beforeConv { get; set; }

        private void ExecuteBasedOnType(Envelope env)
        {
            Envelope.TypeOfMessage msgType = env.MessageTypeInEnvelope;
            Conversation conv;
            switch (msgType)
            {
                case Envelope.TypeOfMessage.CreateGame:
                    conv = ConversationFactory.Instance.CreateFromMessage(env, beforeConv, null) as CreateGameConv;
                    
                    break;

                default:
                    conv = null;
                    break;
            }
            if (conv != null)
            {
                Thread thrd = new Thread(conv.Execute);
                thrd.Start();
            }
        }

        private Envelope Decipher(byte[] bytes)
        {
            Envelope env = null;
            if (bytes != null)
            {
                env = UDPClient.Decode(bytes);
            }

            return env;
        }

        private void CommandSelection(Envelope envelope)
        {
            Command command = null;
            if(envelope != null)
            {
                switch (envelope.MessageTypeInEnvelope)
                {
                    //remove this case later
                    case Envelope.TypeOfMessage.CreateGame:
                        CreateGame msg = envelope.MessageToBeSent as CreateGame;//Message that was received
                        //CommandFactory.Instance.CreateAndExecute("resp");

                        command = new RespCommand();
                        break;

                    case Envelope.TypeOfMessage.Ack:
                        
                        break;
                }
                if(command != null)
                {
                    ReceiveInvoker.EnqueueCommandForExecution(command);
                }
            }
        }
    }
}
