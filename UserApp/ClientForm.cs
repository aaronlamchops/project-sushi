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
using CommSubSystem.Conversations;

namespace UserApp
{
    public partial class ClientForm : Subject
    {
        private static readonly object MyLock = new object();

        private IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1025);
        private IPEndPoint gameServer;
        public ClientReceive _ReceivingProcess;
        public Thread _receivingThread;

        //Hold and populate list from server
        //public List<Game> GameList = new List<Game>();
        public ListViewItem SelectedGame;

        public List<ListViewItem> GameItems = new List<ListViewItem>();
        public bool NeedsRefresh = false;

        public Player Player = new Player();

        public ClientForm()
        {
            InitializeComponent();

            UDPClient.UDPInstance.SetupAndRun(0);
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
            PlayerNameLabel.Text = Player.Name;
            RefreshTimer.Start();
        }

        //call this when the host player wants to start
        private void StartGame()
        {
            StartGame conv = ConversationFactory.Instance
                .CreateFromConversationType<StartGame>
                (server, null, null, null);
            conv._GameId = Player.GameId;
            conv.Start();
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
            /*
             * TESTING:
             * 
             * This button is just testing how the GameUI will look and be created
             * 
             * This can be removed later when we have working implementations
             */
            GameUI ui = new GameUI();
            ui.PlayerList.Add(Player);  //add the user

            //Add temporary filler players
            ui.PlayerList.Add(new Player()
            {
                Name = "Other Player",
                Id = 1
            });
            ui.PlayerList.Add(new Player()
            {
                Name = "That Player",
                Id = 2
            });
            ui.PlayerList.Add(new Player()
            {
                Name = "A Player",
                Id = 3
            });
            ui.PlayerList.Add(new Player()
            {
                Name = "Those Player",
                Id = 4
            });

            //display
            ui.ShowDialog();
        }

        private void CreateGameButton_Click(object sender, EventArgs e)
        {
            var CreateGameForm = new CreateGameForm();

            if(CreateGameForm.ShowDialog() == DialogResult.OK)
            {
                CreateGame(CreateGameForm.MinPlayerCount, CreateGameForm.TotalPlayerCount, CreateGameForm.GameName);
            }
        }

        public void CreateGame(int min, int max, string name)
        {
            //var parameters = new string[]{ min.ToString(), max.ToString(), name };

            CreateGameConv conv =
                ConversationFactory.Instance
                .CreateFromConversationType<CreateGameConv>
                (server, null, CreateGamePostExecute, null); //using lambda operator to pass parameters as object

            conv._GameName = name;
            conv._MinPlayers = min;
            conv._MaxPlayers = max;
            conv._Player = Player;

            conv.Start();
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            if (SelectedGame == null) return;

            JoinGameConv conv = 
                ConversationFactory
                    .Instance.CreateFromConversationType<JoinGameConv>
                    (server, null, JoinGamePostExecute, null);

            Thread convThread = new Thread(conv.Execute);

            //Add values to conversation from selected game
            conv._GameId = Int32.Parse(SelectedGame.SubItems[1].Text);
            conv._Player = Player;

            convThread.Start();
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
            if(ReceivingListView.SelectedIndices.Count == 1)
            {
                SelectedGame = GameItems[ReceivingListView.SelectedIndices[0]];
            }
            else
            {
                SelectedGame = null;
            }
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
            string[] parameters = ((IEnumerable)context)
                .Cast<object>().Select(x => x.ToString()).ToArray();


            Player.IsHost = true;
            Player.InWaitingRoom = true;
            Player.GameId = Int32.Parse(parameters[3]);

            var waitingRoomWindow = new WaitingRoom()
            {
                ID = Player.GameId,
                MinPlayers = Convert.ToInt32(parameters[0]),
                MaxPlayers = Convert.ToInt32(parameters[1]),
                GameName = parameters[2],
                StartGame = StartGame
            };

            Subscribe(waitingRoomWindow);   //newly made waiting room will subscribe to the client

            waitingRoomWindow.ShowDialog();
        }

        public void JoinGamePreExecute(object context)
        {

        }

        public void JoinGamePostExecute(object context)
        {
            GameInfo parameter = (GameInfo)context;

            Player.GameId = parameter.gameId;
            Player.IsHost = false;
            Player.InWaitingRoom = true;

            var waitingRoomWindow = new WaitingRoom()
            {
                ID = parameter.gameId,
                MinPlayers = parameter.MinPlayers,
                MaxPlayers = parameter.MaxPlayers,
                GameName = parameter.GameName,
                StartGame = StartGame
            };

            Subscribe(waitingRoomWindow);   //newly made waiting room will subscribe to the client

            waitingRoomWindow.ShowDialog();
        }

        public void RefreshPostExecute(object context)
        {
            var gameList = (ConcurrentDictionary<int, GameInfo>)context;
            GameItems = new List<ListViewItem>();

            foreach (KeyValuePair<int, GameInfo> index in gameList)
            {
                string[] row = {
                    index.Value.GameName,
                    index.Value.gameId.ToString(),
                    index.Value.playerList.Count.ToString(),
                    index.Value.MaxPlayers.ToString()
                };

                var ListViewItem = new ListViewItem(row);
                GameItems.Add(ListViewItem);
            }
            NeedsRefresh = true;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            PlayerOptions options = new PlayerOptions()
            {
                PlayerName = Player.Name
            };
            if (options.ShowDialog() == DialogResult.OK)
            {
                Player.Name = options.PlayerName;
                PlayerNameLabel.Text = Player.Name;
            }
        }
    }
}
