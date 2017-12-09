using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommSubSystem.Conversations;
using CommSubSystem.ConversationClass;

namespace UserApp
{
    public partial class WaitingRoom : Form
    {
        public string GameName 
        { 
            get { return GameNameLabel.Text; } 
            set { GameNameLabel.Text = value; } 
        }

        public int MinPlayers
        { 
            get { return Convert.ToInt32(MinPlayerCountLabel.Text); } 
            set { MinPlayerCountLabel.Text = value.ToString(); }
        }

        public int MaxPlayers
        { 
            get { return Convert.ToInt32(MaxPlayerCountLabel.Text); }
            set { MaxPlayerCountLabel.Text = value.ToString(); }
        }

        public int PlayerCount
        { 
            get { return Convert.ToInt32(PlayerCountLabel.Text); }
            set { PlayerCountLabel.Text = value.ToString(); }
        }

        public WaitingRoom()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            //need game info to send
            /*StartGame conv = ConversationFactory.Instance
                .CreateFromConversationType<StartGame>(server, null, null, null);
            conv._GameId = gameId;
            conv.Start();*/
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

        }
    }
}
