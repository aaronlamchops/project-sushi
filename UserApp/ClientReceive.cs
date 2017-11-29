using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Messages;
using SharedObjects;
using CommSubSystem.ConversationClass;

namespace CommSubSystem.Receive
{
    public class ClientReceive
    {
        private bool _keepReceiving;

        public void Receiving()
        {
            byte[] bytes;
            Envelope env = null;
            while (_keepReceiving)
            {
                bytes = UDPClient.UDPInstance.Receive();
                env = Decipher(bytes);

                if (env != null)
                {
                    MessageId convId = env.MessageToBeSent.ConvId;
                    ConversationQueue queue = ConversationDictionary.Instance.Lookup(convId);
                    if (queue == null)
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
    }

    
}
