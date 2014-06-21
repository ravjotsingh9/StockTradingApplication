using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
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
            Form1 Client = new Form1();
            Client.Text = "Get Stock Value";
            Client.SetDesktopLocation(500, 1);
            Application.Run(Client);
        }
    }
}
