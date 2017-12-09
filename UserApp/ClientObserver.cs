using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp
{
    public class ClientObserver : Form
    {
        private readonly Timer _RefreshTimer = new Timer();
        private readonly object _MyLock = new object();
        protected bool NeedsRefresh;

        //object that can be anything that is need to update
        public object MainContext = new object();

        public void StartRefreshTimer()
        {
            _RefreshTimer.Interval = (1000);
            _RefreshTimer.Tick += new EventHandler(Refresh_Tick);
            _RefreshTimer.Start();
        }

        public virtual void Update(object context)
        {
            lock (_MyLock)
            {
                MainContext = context;
                NeedsRefresh = true;
            }
        }

        private void Refresh_Tick(object sender, EventArgs e)
        {
            if (NeedsRefresh)
            {
                lock (_MyLock)
                {
                    RefreshDisplay();
                    NeedsRefresh = false;
                }
            }
        }

        //implemented by inheriting classes
        public virtual void RefreshDisplay() { }

        

        
    }
}
