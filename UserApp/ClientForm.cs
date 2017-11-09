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
using Messages;
using SharedObjects;

namespace UserApp
{
    public partial class ClientForm : Form
    {
        private readonly ControlHub _ControlHub = new ControlHub();
        private readonly SendInvoker _SendingInvoker = new SendInvoker();

        public Thread _receivingThread;

        private readonly bool _keepReceiving;

        public ClientForm()
        {
            InitializeComponent();

            _ControlHub = new ControlHub();
            CommandFactory.Instance.SendInvoker = _SendingInvoker;
            CommandFactory.Instance.TargetControl = _ControlHub;

            _SendingInvoker.Start();


            //receiving thread
            _receivingThread = new Thread(Receive);
            _keepReceiving = true;
            _receivingThread.Start();
        }

        public void Receive()
        {
            byte[] bytes;
            Envelope env = null;
            while(_keepReceiving)
            {
                bytes = UDPClient.UDPInstance.Receive();
                env = UDPClient.Decode(bytes);

                if(env != null)
                {
                    string row = "Received";
                    var listViewItem = new ListViewItem(row);
                    ReceivingListView.Items.Add(listViewItem);
                }
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            refreshTimer.Start();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //call a method that needs to be refreshed a second
            //something that needs to redraw
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            //gets the address, port, and message to be sent from the textfields
            CommandFactory.Instance.CreateAndExecute("send", AddressTextBox.Text, textBox2.Text);
        }

    }
}
