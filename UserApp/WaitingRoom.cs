using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp
{
    public partial class WaitingRoom : Form
    {
        public string GameName { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public int PlayerCount { get; set; }

        public WaitingRoom()
        {
            GameNameLabel.Text = GameName;
            MinPlayerCountLabel.Text = MinPlayers.ToString();
            MaxPlayerCountLabel.Text = MaxPlayers.ToString();
            PlayerCountLabel.Text = PlayerCount.ToString();
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

        }
    }
}
