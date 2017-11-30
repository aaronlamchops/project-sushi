using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedObjects
{
    public class LocalProcessInfo
    {
        private static LocalProcessInfo _instance;
        private static readonly object MyLock = new object();
        private LocalProcessInfo()
        {
            ProcessId = 0;
        }

        public static LocalProcessInfo Instance
        {
            get
            {
                lock (MyLock)
                {
                    if (_instance == null)
                        _instance = new LocalProcessInfo();
                }
                return _instance;
            }
        }

        public short ProcessId { get; set; }

        public DateTime StartTime { get; set; }
    }
}
