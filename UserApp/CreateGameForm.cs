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
    public partial class CreateGameForm : Form
    {
        public CreateGameForm()
        {
            InitializeComponent();
        }

        public string GameName
        {
            get { return NameTextBox.Text; }
            set { NameTextBox.Text = value; }
        }

        public int TotalPlayerCount { get; set; }
        public int MinPlayerCount { get; set; }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void TotalPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            TotalPlayerCount = Convert.ToInt32(cmb.SelectedItem.ToString());
        }

        private void MinPlayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            MinPlayerCount = Convert.ToInt32(cmb.SelectedItem.ToString());
        }
    }
}
