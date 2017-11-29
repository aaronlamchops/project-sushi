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

        protected override void ExecuteBasedOnType(Envelope env)
        {
            Envelope.TypeOfMessage msgType = env.MessageTypeInEnvelope;
            CreateGameConv conv = null;
            switch (msgType)
            {
                case Envelope.TypeOfMessage.CreateGame:
                    conv = ConversationFactory.Instance.CreateFromMessage(env, null, null) as CreateGameConv;
                    conv._GameId = ManageGameID();
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
    }    
}
