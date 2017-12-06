using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp
{
    public class HandleStartup
    {
        public bool LaunchUserApp { get; set; }
        public string Name { get; set; }

        public void Startup()
        {
            var PlayerOption = new PlayerOptions();

            if(PlayerOption.ShowDialog() == DialogResult.OK)
            {
                if(String.IsNullOrEmpty(PlayerOption.PlayerName))
                {
                    MessageBox.Show("Please enter a name in the textfield.");
                }
                else
                {
                    Name = PlayerOption.PlayerName;
                    LaunchUserApp = true;
                }
            }
            
        }

    }
}
