using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.ObjectModel;
using Server.Yahoo_Finance;

namespace Server
{
    public partial class Form1 : Form
    {
        ServerMainThread serverthread;

        public Form1()
        {
            serverthread = new ServerMainThread();
            InitializeComponent();
            Stock obj = new Stock();
        }

        private void button1_Click(object sender, EventArgs e)
        {    
            label1.Text = "Running";
            button1.Enabled = false;
            //button2.Enabled = true;
            serverthread.startMainThread();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serverthread.stopMainThread();
            //button2.Enabled = false;
            button1.Enabled = true;
        }
        
    }

}
