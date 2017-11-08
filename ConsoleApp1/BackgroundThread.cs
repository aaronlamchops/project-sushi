using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Utils
{
    public abstract class BackgroundThread : IDisposable
    {
        #region Private data members
        private bool _suspended;

        protected bool KeepGoing { get; set; }

        #endregion

        #region Constructors and destruction
        public void Dispose()
        {
            Stop();
        }
        #endregion

        #region Public Properties and Methods
        public string Label { get; set; }

        public virtual void Start()
        {

            try
            {
                KeepGoing = true;
                _suspended = false;
                ThreadPool.QueueUserWorkItem(Process, null);
            }
            catch (Exception err)
            {
                //error
            }
        }

        public virtual void Stop()
        {
            KeepGoing = false;                              // Clear the flag that keep the background
            Thread.Sleep(0);                                // Give up the processor so other threads will run
        }

        public virtual string Status
        {
            get
            {
                string result = "Not running";
                if (KeepGoing)
                    result = (_suspended) ? "Suspended" : "Running";
                return result;
            }
        }

        public virtual bool Suspended
        {
            get { return _suspended; }
            set
            {
                _suspended = value;
            }
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Main process method for background thread
        /// 
        /// This method should stop whatever it is doing and terminate whenever KeepGoing becomes false. 
        /// Also, it should not actually do any process anything but stay alive, if suspend becomes true.
        /// </summary>
        protected abstract void Process(Object state);

        #endregion
    }
}
