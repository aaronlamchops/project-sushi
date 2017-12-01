using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommSubSystem;
using CommSubSystem.ConversationClass;
using Messages;

namespace GameApp
{
    public class GameReceive : Receiver
    {
        protected override void ExecuteBasedOnType
            (byte[] bytes, TypeOfMessage type, IPEndPoint refEp)
        {
            Conversation conv = null;
            switch (type)
            {
                case TypeOfMessage.ConnectGameServerMsg:
                    break;
            }
            if(conv != null)
            {
                conv.Start();
            }
        }
    }
}
