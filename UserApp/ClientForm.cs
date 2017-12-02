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

using CommSubSystem;
using SharedObjects;
using System.Net;
using CommSubSystem.ConversationClass;

namespace UserApp
{
    public partial class ClientForm : Form
    {
        private static readonly object MyLock = new object();

        IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1025);
        public ClientReceive _ReceivingProcess;
        public Thread _receivingThread;

        //Hold and populate list from server
        public List<Game> GameList = new List<Game>();
        public Game SelectedGame;

        public List<ListViewItem> GameItems = new List<ListViewItem>();
        public bool NeedsRefresh = false;

        public ClientForm()
        {
            InitializeComponent();

            UDPClient.UDPInstance.SetupAndRun(1024);
            _ReceivingProcess = new ClientReceive();
            _ReceivingProcess.Start();

            //get pid
            while(LocalProcessInfo.Instance.ProcessId == 0)
            {
                GetPid();
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            RefreshTimer.Start();
        }

        private void ClientForm_FormClosed(object sender, EventArgs e)
        {
            _ReceivingProcess.Stop();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            //call a method that needs to be refreshed a second
            //something that needs to redraw
            RefreshGameList();
        }

        public void RefreshGameList()
        {
            if(NeedsRefresh)
            {
                ReceivingListView.Items.Clear();

                foreach(ListViewItem item in GameItems)
                {
                    ReceivingListView.Items.Add(item);
                }
            }

            NeedsRefresh = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {

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
            var parameters = new string[]{ min.ToString(), max.ToString(), name };

            CreateGameConv conv =
                ConversationFactory.Instance
                .CreateFromConversationType<CreateGameConv>
                (server, null, param => CreateGamePostExecute(parameters), null); //using lambda operator to pass parameters as object

            conv._GameName = name;
            conv._MinPlayers = min;
            conv._MaxPlayers = max;

            conv.Start();
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            if (SelectedGame == null) return;

            //send and connect to lobby using selected lobby information
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RequestGameListConv conv = 
                ConversationFactory
                .Instance.CreateFromConversationType<RequestGameListConv>
                (server, null, RefreshPostExecute, null);

            Thread convThread = new Thread(conv.Execute);
            convThread.Start();
        }

        private void ReceivingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(ReceivingListView.SelectedIndices.Count == 1)
            //{
            //    SelectedGame = GameList[ReceivingListView.SelectedIndices[0]];
            //}
            //else
            //{
            //    SelectedGame = null;
            //}
        }

        public void GetPid()
        {
            Registration conv = ConversationFactory.Instance
                .CreateFromConversationType<Registration>
                (server, null, null, null);
            conv.Start();
            while (conv.Done != true)
            {
            }
        }

        public void CreateGamePreExecute(object context)
        {

        }

        public void CreateGamePostExecute(object context)
        {
            //change player screeen
            //player.inWaitingRoom = true
            string[] parameters = ((IEnumerable)context).Cast<object>().Select(x => x.ToString()).ToArray();

            var waitingRoomWindow = new WaitingRoom()
            {
                MaxPlayers = Convert.ToInt32(parameters[0]),
                MinPlayers = Convert.ToInt32(parameters[1]),
                GameName = parameters[2],
            };

            waitingRoomWindow.ShowDialog();
        }

        public void JoinGamePreExecute(object context)
        {

        }

        public void JoinGamePostExecute(object context)
        {

        }

        public void RefreshPostExecute(object context)
        {
            var gameList = (ConcurrentDictionary<int, Game>)context;
            GameItems = new List<ListViewItem>();

            foreach (KeyValuePair<int, Game> index in gameList)
            {
                string[] row = {
                    index.Value.gameId.ToString(),
                    index.Value.gameId.ToString(),
                    index.Value.playerList.Count.ToString(),
                    index.Value.MaxPlayers.ToString()
                };

                var ListViewItem = new ListViewItem(row);
                GameItems.Add(ListViewItem);
            }
            NeedsRefresh = true;
        }
        
    }
}
