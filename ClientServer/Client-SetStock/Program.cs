using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_SetStock
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
            Form1 ClientSet = new Form1();
            ClientSet.Text = "Set Stocks";
            ClientSet.SetDesktopLocation(100,1);
            Application.Run(ClientSet);
        }
    }
}
