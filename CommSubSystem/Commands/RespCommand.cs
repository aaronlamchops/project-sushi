using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using SharedObjects;

using Messages;
using CommSubSystem;

namespace CommSubSystem.Commands
{
    public class RespCommand : Command
    {
        private string messageToBeSent;
        private string address;
        private string port;

        internal RespCommand() { }

        internal RespCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
            {
                address = (string)commandParameters[0];
            }
            if (commandParameters.Length > 1)
            {
                port = (string)commandParameters[1];
            }
        }

        public override void Execute()
        {
            //create a message out of this
            Ack msg = new Ack();
            msg.ConvId = MessageId.Create();
            Envelope env = new Envelope(msg, UDPClient.UDPInstance.GetEndPoint());
            env.MessageTypeInEnvelope = Envelope.TypeOfMessage.Ack;

            byte[] bytes = env.Encode();

            UDPClient.UDPInstance.Send(bytes);

            //TargetControl.SetupConversation(msg.ConvId, env);
        }
    }
}
