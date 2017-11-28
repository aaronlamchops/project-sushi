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

namespace LobbyApp
{
    public partial class ClientForm : Form
    {
        private readonly ControlHub _ControlHub = new ControlHub();
        private readonly SendInvoker _SendingInvoker = new SendInvoker();
        private ConversationQueue _receivingQueue = new ConversationQueue();

        public Thread _receivingThread;
        public Thread _receivingQueueThread;
        private bool _keepReceiving;

        public ClientForm()
        {
            InitializeComponent();

            UDPClient.UDPInstance.SetupAndRun(5);
            _ControlHub = new ControlHub();
            CommandFactory.Instance.SendInvoker = _SendingInvoker;
            CommandFactory.Instance.TargetControl = _ControlHub;

            _SendingInvoker.Start();

            _keepReceiving = true;
            //receiving thread
            _receivingThread = new Thread(Receive);
            _receivingThread.IsBackground = true;
            _receivingThread.Start();
            //receiving queue thread
            _receivingQueueThread = new Thread(ReceiveQueue);
            _receivingQueueThread.IsBackground = true;
            _receivingQueueThread.Start();
        }

        public void Receive()
        {
            byte[] bytes;
            Envelope env = null;
            string row = "";
            while (_keepReceiving)
            {
                bytes = UDPClient.UDPInstance.Receive();
                if (bytes != null)
                {
                    env = UDPClient.Decode(bytes);
                    _receivingQueue.Enqueue(env);
                }
            }
        }

        public void ReceiveQueue()
        {
            Envelope env = null;
            string row = "";
            while (_keepReceiving)
            {
                env = _receivingQueue.Dequeue(5);
                if (env != null)
                {
                    switch (env.MessageTypeInEnvelope)
                    {
                        case Envelope.TypeOfMessage.CreateGame:
                            row = "createGame";
                            CreateGame msg = env.MessageToBeSent as CreateGame;
                            CommandFactory.Instance.CreateAndExecute("resp", AddressTextBox.Text, textBox2.Text);
                            break;
                        case Envelope.TypeOfMessage.Ack:
                            row = "ack";
                            break;
                    }
                }

                //if (env != null)
                //{

                //    var listViewItem = new ListViewItem(row);
                //    ReceivingListView.Items.Add(listViewItem);
                //}
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

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

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _keepReceiving = false;
            _receivingThread.Join();
            _receivingQueueThread.Join();

        }
    }
}
