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
using System.Net;
using CommSubSystem.Conversation;

namespace UserApp
{
    public partial class ClientForm : Form
    {
        private static readonly object MyLock = new object();
        
        private readonly SendInvoker _SendingInvoker = new SendInvoker();
        private readonly ReceiveInvoker _ReceivingInvoker = new ReceiveInvoker();

        public Thread _receivingThread;

        //Hold and populate list from server
        public List<Game> GameList = new List<Game>();
        public Game SelectedGame;

        public ClientForm()
        {
            InitializeComponent();

            UDPClient.UDPInstance.SetupAndRun(1024);
            CommandFactory.Instance.SendInvoker = _SendingInvoker;
            ReceivingFactory.Instance.ReceiveInvoker = _ReceivingInvoker;
            

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

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //call a method that needs to be refreshed a second
            //something that needs to redraw
        }

        public void RefreshGameList()
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            //gets the address, port, and message to be sent from the textfields
            CommandFactory.Instance.CreateAndExecute("send", AddressTextBox.Text, textBox2.Text);
        }

        private void CreateGameButton_Click(object sender, EventArgs e)
        {
            var CreateGameForm = new CreateGameForm();

            if(CreateGameForm.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show("Total Players = " + CreateGameForm.TotalPlayerCount.ToString() + "\nGame Name: " + CreateGameForm.GameName);

                CreateGame(CreateGameForm.MinPlayerCount, CreateGameForm.TotalPlayerCount, CreateGameForm.GameName);
            }
        }

        public void CreateGame(int min, int max, string name)
        {
            string[] parameters = { min.ToString(), max.ToString(), name };

            IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1025);
            CreateGameConv conv =
                ConversationFactory.Instance
                .CreateFromConversationType<CreateGameConv>
                (server, null, param => CreateGamePostExecute(parameters)); //using lambda operator to pass parameters as object

            conv.GameName = name;
            conv.MinPlayers = min;
            conv.MaxPlayers = max;

            Thread convThread = new Thread(conv.Execute);
            convThread.Start();
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            if (SelectedGame == null) return;

            //send and connect to lobby using selected lobby information
        }

        private void ReceivingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ReceivingListView.SelectedIndices.Count == 1)
            {
                SelectedGame = GameList[ReceivingListView.SelectedIndices[0]];
            }
            else
            {
                SelectedGame = null;
            }
        }

        public void CreateGamePreExecute(object context)
        {

        }

        public void CreateGamePostExecute(object context)
        {
            //change player screeen
            //player.inWaitingRoom = true
            string[] parameters = (string[])context;

            var waitingRoomWindow = new WaitingRoom()
            {
                MinPlayers = Convert.ToInt32(parameters[0]),
                MaxPlayers = Convert.ToInt32(parameters[1]),
                GameName = parameters[2]
            };
            waitingRoomWindow.ShowDialog();
        }

        public void JoinGamePreExecute(object context)
        {

        }

        public void JoinGamePostExecute(object context)
        {

        }

        
    }
}
