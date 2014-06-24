using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        //this is a testing statement to see whether i can commit or not- Amulya
        // Hi I'm here. -for testing Claire
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 Client = new Form1();
            Client.Text = "Get Stock Value";
            Client.SetDesktopLocation(500, 1);
            Application.Run(Client);
        }
    }
}
