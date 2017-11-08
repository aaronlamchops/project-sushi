using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommSubSystem.Commands
{
    public abstract class Command
    {
        public ControlHub TargetControl { get; set; }

        public abstract void Execute();     //could be type bool to check succeed or fail
    }
}
