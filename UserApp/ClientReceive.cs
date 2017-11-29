using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Messages;
using SharedObjects;
using CommSubSystem;
using CommSubSystem.ConversationClass;
using log4net;

namespace UserApp
{
    public class ClientReceive : Receiver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClientReceive));

        protected override void ExecuteBasedOnType(Envelope env)
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
    }

    
}
