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
                lock (MyLock)
                {
                    if (_Instance == null)
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
            //_worker = new Thread(Receive);
            //_worker.Start();
        }

        public void Stop()
        {
            _keepReceiving = false;
        }

        private void CommandSelection(Envelope envelope)
        {
            Command command = null;
            if (envelope != null)
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
                if (command != null)
                {
                    ReceiveInvoker.EnqueueCommandForExecution(command);
                }
            }
        }
    }
}
