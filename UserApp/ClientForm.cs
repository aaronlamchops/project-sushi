using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using CommSubSystem;
using CommSubSystem.Commands;
using CommSubSystem.Receive;
using Messages;
using SharedObjects;

namespace UserApp
{
    public partial class ClientForm : Form
    {
        private static ClientForm _Instance;
        private static readonly object MyLock = new object();

        private readonly ControlHub _ControlHub = new ControlHub();
        private readonly SendInvoker _SendingInvoker = new SendInvoker();
        private readonly ReceiveInvoker _ReceivingInvoker = new ReceiveInvoker();

        public Thread _receivingThread;


        public static ClientForm Instance
        {
            get
            {
                lock(MyLock)
                {
                    if(_Instance == null)
                    {
                        _Instance = new ClientForm();
                    }
                }
                return _Instance;
            }
        }

        private ClientForm()
        {
            InitializeComponent();

            UDPClient.UDPInstance.SetupAndRun(30);
            _ControlHub = new ControlHub();
            CommandFactory.Instance.SendInvoker = _SendingInvoker;
            CommandFactory.Instance.TargetControl = _ControlHub;
            ReceivingFactory.Instance.ReceiveInvoker = _ReceivingInvoker;
            ReceivingFactory.Instance.TargetControl = _ControlHub;

            _SendingInvoker.Start();
            _ReceivingInvoker.Start();

            //kick off receiving for the whole system
            ReceivingFactory.Instance.Start();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            //System.Windows.Forms.Timer RefreshTimer = new System.Windows.Forms.Timer();
            //RefreshTimer.Interval = 1000; //every second
            //RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            //RefreshTimer.Start();
        }

        private void ClientForm_FormClosed(object sender, EventArgs e)
        {
            _SendingInvoker.Stop();
            _ReceivingInvoker.Stop();
            ReceivingFactory.Instance.Stop();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            //call a method that needs to be refreshed a second
            //something that needs to redraw

            //if(_ControlHub.ForceRedraw)
            //{
            //    ReceivingListView.Items.Clear();
            //}

            //_ControlHub.ForceRedraw = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            //gets the address, port, and message to be sent from the textfields
            CommandFactory.Instance.CreateAndExecute("send", AddressTextBox.Text, textBox2.Text);
        }
    }
}
