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
using Messages;
using SharedObjects;
using System.Net;
using CommSubSystem.ConversationClass;
using System.Diagnostics;

namespace LobbyApp
{
    public partial class ClientForm : Form
    {
        public Thread _receivingThread;
        public LobbyReceive _ReceivingProcess;

        private static readonly object MyLock = new object();
        

        private Lobby _lobby = new Lobby();

        public ClientForm()
        {
            InitializeComponent();
            UDPClient.UDPInstance.SetupAndRun(1025);

            LocalProcessInfo.Instance.ProcessId = 1;

            _ReceivingProcess = new LobbyReceive();
            _ReceivingProcess.Start();
        }

        

        private void ClientForm_Load(object sender, EventArgs e)
        {
        }

        private void LobbyForm_FormClosed(object sender, EventArgs e)
        {
            _ReceivingProcess.Stop();
        }

        //where we would put the createGame stuff
        public void CreateGame()
        {
            IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
            CreateGameConv conv =
                ConversationFactory.Instance
                .CreateFromConversationType<CreateGameConv>
                (server, null, GameCreated);
            Thread convThread = new Thread(conv.Execute);
            convThread.Start();
        }

        public void GameCreated(object context)
        {
            //change player screeen
            //player.inWaitingRoom = true
        }


        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //call a method that needs to be refreshed a second
            //something that needs to redraw
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            //gets the address, port, and message to be sent from the textfields
            IPEndPoint playerIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1024);
            LobbyHeartbeatConv conv =
                ConversationFactory.Instance
                .CreateFromConversationType<LobbyHeartbeatConv>
                (playerIP, null, null);
            conv._NumberOfPlayers = 3;
            Thread convThread = new Thread(conv.Execute);
            convThread.Start();
        }
    }
}
