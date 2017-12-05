using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommSubSystem;
using CommSubSystem.ConversationClass;
using CommSubSystem.Conversations;
using Messages;
using SharedObjects;

namespace GameApp
{
    public class GameReceive : Receiver
    {
        private List<Game> gameList = new List<Game>();

        protected override void ExecuteBasedOnType
            (byte[] bytes, TypeOfMessage type, IPEndPoint refEp)
        {
            switch (type)
            {
                case TypeOfMessage.ConnectGameServerMsg:
                    ConnectGameServerResponse(bytes, refEp);
                    break;
            }
        }

        public void ConnectGameServerResponse(byte[] bytes, IPEndPoint refEp)
        {
            ConnectGameServer conv = ConversationFactory.Instance.
                CreateFromMessage<ConnectGameServer>(bytes, refEp, null, null, null);
            conv.Start();

            ConnectGSMsg convMessage = Message.Decode<ConnectGSMsg>(bytes);
            TCPClient client = new TCPClient();
            IPEndPoint clientTcp = refEp;
            clientTcp.Port = convMessage.Port;
            client.ConnectToServer(clientTcp);
            //need to store client tcp somewhere
        }
    }
}
