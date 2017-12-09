using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommSubSystem;
using System.Threading;
using CommSubSystem.ConversationClass;
using System.Net;
using SharedObjects;

namespace GameApp
{
    public partial class GameServer : Form
    {
        IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1025);
        public GameReceive _ReceivingProcess;
        public Thread _receivingThread;

        public GameServer()
        {
            InitializeComponent();

            UDPClient.UDPInstance.SetupAndRun(1026);
            _ReceivingProcess = new GameReceive();
            _ReceivingProcess.Start();

            //get pid
            while (LocalProcessInfo.Instance.ProcessId == 0)
            {
                GetPid();
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
    }
}
