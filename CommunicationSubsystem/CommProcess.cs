using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedObjects;
using Utils;

namespace CommunicationSubsystem
{
    public abstract class CommProcess : BackgroundThread
    {
        protected const int MainLoopSleep = 200;
        protected object MyLock = new object();

        public RuntimeOptions Options { get; set; }
        public CommSubSystem MyCommSubSystem { get; protected set; }
        public string BestLocalEndPoint => MyCommSubSystem?.BestLocalEndPoint;

        protected virtual void SetupCommSubsystem(CommProcessState processState, ConversationFactory conversationFactory)
        {
            MyCommSubSystem = new CommSubSystem(processState, conversationFactory);
            MyCommSubSystem.Initialize();
            MyCommSubSystem.Start();
        }

        public override void Stop()
        {
            base.Stop();

            MyCommSubSystem.Stop();
            RaiseShutdownEvent();

        }

        public event StateChange.Handler Shutdown;

        public void RaiseShutdownEvent()
        {
            if(Shutdown != null)
            {
                Shutdown(new StateChange() { Type = StateChange.ChangeType.Shutdown, Subject = this });
            }
            else
            {
                //log out error
            }
        }
    }
}
