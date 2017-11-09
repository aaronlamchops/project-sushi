using System;


namespace CommSubSystem.Commands
{
    public class SendCommand : Command
    {
        private string messageToBeSent;
        private string address;
        private string port;

        internal SendCommand() { }

        internal SendCommand(params object[] commandParameters)
        {
            if(commandParameters.Length > 0)
            {
                address = (string)commandParameters[0];
            }
            if (commandParameters.Length > 1)
            {
                address = (string)commandParameters[1];
            }
        }

        public override void Execute()
        {
            //create a message out of this

            //create an envelope

            //send the message

            //control the conversation with the queue
        }
    }
}
