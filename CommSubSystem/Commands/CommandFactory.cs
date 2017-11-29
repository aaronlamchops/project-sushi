using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommSubSystem.Commands
{
    public class CommandFactory
    {
        private static CommandFactory _Instance;
        private static readonly object MyLock = new object();
        private CommandFactory() { }

        public static CommandFactory Instance
        {
            get
            {
                lock(MyLock)
                {
                    if(_Instance == null)
                    {
                        _Instance = new CommandFactory();
                    }
                }
                return _Instance;
            }
        }

        //public ControlHub TargetControl { get; set; }
        public SendInvoker SendInvoker { get; set; }


        public virtual void CreateAndExecute(string commandType, params object[] commandParameters)
        {
            if(string.IsNullOrWhiteSpace(commandType))
            {
                return;
            }
            //if(TargetControl == null)
            //{
            //    return;
            //}

            Command command = null;

            switch(commandType.Trim().ToUpper())
            {
                case "SEND":
                    command = new SendCommand(commandParameters);
                    break;

                //Might keep receiving commands in the Receiving Factory
                case "RESP":
                    command = new RespCommand(commandParameters);
                    break;

            }

            if(command != null)
            {
                //command.TargetControl = TargetControl;
                SendInvoker.EnqueueCommandForExecution(command);
            }
        }

    }
}
