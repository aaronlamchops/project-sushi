using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Messages;
using SharedObjects;
using CommSubSystem.ConversationClass;
using log4net;
using CommSubSystem;
using System.Net;

using System.Diagnostics;

namespace LobbyApp
{
    public class LobbyReceive : Receiver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LobbyReceive));
        private static readonly object MyLock = new object();

        private int _GameID { get; set; }
        

        private int ManageGameID()
        {
            lock (MyLock)
            {
                _GameID++;
            }

            return _GameID;
        }

        protected override void ExecuteBasedOnType(Envelope env, IPEndPoint refEp)
        {
            Envelope.TypeOfMessage msgType = env.MessageTypeInEnvelope;
            Conversation conv = null;
            switch (msgType)
            {
                case Envelope.TypeOfMessage.CreateGame:
                    conv = CreateGameResponse(env, refEp);
                    break;
                case Envelope.TypeOfMessage.Ack:
                    Debug.WriteLine("ACK");
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

        private Conversation CreateGameResponse(Envelope env, IPEndPoint refEp)
        {
            CreateGameConv conv = ConversationFactory.Instance.CreateFromMessage(env, refEp, null, null) as CreateGameConv;
            conv._GameId = ManageGameID();
            return conv;
        }
    }    
}
