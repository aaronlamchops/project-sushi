using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharedObjects;

namespace UserApp
{
    public partial class PlayerOptions : Form
    {
        public string PlayerName
        {
            get
            {
                return PlayerNameTextBox.Text;
            }
            set
            {
                PlayerNameTextBox.Text = value;
            }
        }

        public PlayerOptions()
        {
            InitializeComponent();
        }
    }
}
