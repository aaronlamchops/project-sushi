using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserApp
{
    /*
     * THIS SHOULD BE OUR ONLY PROGRAM THAT HAS MAIN()
     * This is the top GUI layer in which this layer references all other layers
     */
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            HandleStartup Handler = new HandleStartup();
            Handler.Startup();
            if(Handler.LaunchUserApp)
            {
                Application.Run(new ClientForm()
                {
                    Player = new SharedObjects.Player()
                    {
                        Name = Handler.Name,
                        Id = 0
                    }
                });
            }
        }
    }
}
