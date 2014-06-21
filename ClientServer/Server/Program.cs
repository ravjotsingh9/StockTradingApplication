using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Server
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
            Form1 Server = new Form1();
            Server.Text = "Server";
            Server.SetDesktopLocation(1, 1);
            Server.TopMost = true;
            Application.Run(Server);
        }
    }
}
