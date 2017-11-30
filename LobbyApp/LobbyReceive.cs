using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CommSubSystem.ConversationClass;
using log4net;
using CommSubSystem;
using System.Net;
using Messages;

using System.Diagnostics;

namespace LobbyApp
{
    public class LobbyReceive : Receiver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LobbyReceive));
        private static readonly object MyLock = new object();

        private int _GameID { get; set; }
        private short _Pid { get; set; }

        public LobbyReceive()
        {
            _Pid = 1;
        }

        private short ManageProcessID()
        {
            _Pid++;
            return _Pid;
        }

        private int ManageGameID()
        {
            lock (MyLock)
            {
                _GameID++;
            }

            return _GameID;
        }

        protected override void ExecuteBasedOnType(byte[] bytes, TypeOfMessage type, IPEndPoint refEp)
        {
            Conversation conv = null;
            switch (type)
            {
                case TypeOfMessage.CreateGame:
                    conv = CreateGameResponse(bytes, refEp);
                    break;
                //case TypeOfMessage.Registration:
                    //conv = RegistrationResponse(bytes, refEp);
                    //break;
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

        private CreateGameConv CreateGameResponse(byte[] bytes, IPEndPoint refEp)
        {
            CreateGameConv conv = ConversationFactory.Instance.CreateFromMessage<CreateGameConv>(bytes, refEp, null, null);
            conv._GameId = ManageGameID();
            return conv;
        }

        //private Registration RegistrationResponse(byte[] bytes, IPEndPoint refEp)
        //{
        //    Registration conv = ConversationFactory.Instance.CreateFromMessage<Registration>(bytes, refEp, null, null);
        //    conv._processId = ManageProcessID();
        //    return conv;
        //}
    }    
}
