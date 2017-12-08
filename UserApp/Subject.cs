using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp
{
    public class Subject : Form
    {
        private readonly object _MyLock = new object();

        public List<ClientObserver> Subscribers { get; } = new List<ClientObserver>();

        public void Subscribe(ClientObserver observer)
        {
            lock (_MyLock)
            {
                if(observer != null && !Subscribers.Contains(observer))
                {
                    Subscribers.Add(observer);
                }
            }
        }

        public void Unsubscribe(ClientObserver observer)
        {
            lock (_MyLock)
            {
                if(Subscribers.Contains(observer))
                {
                    Subscribers.Remove(observer);
                }
            }
        }

        public void Notify(object context)
        {
            lock (_MyLock)
            {
                foreach(ClientObserver observer in Subscribers)
                {
                    observer.Update(context);
                }
            }
        }

        public virtual Subject Clone()
        {
            return MemberwiseClone() as Subject;
        }
    }
}
