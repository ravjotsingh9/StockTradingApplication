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

namespace Server
{
    public partial class Form1 : Form
    {
        string data;
        Socket soc;
        Socket listener;
        List<Stock> Stocklist;
        //volatile bool stop;
        Thread thread;
        public Form1()
        {
            Stocklist  = new List<Stock>();
            InitializeComponent();
            Stock obj = new Stock();
            obj.stockname = "abc";
            obj.stockprice = 10;
            Stocklist.Add(obj);
            
            //stop = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(serverthread));
            label1.Text = "Running";
            button1.Enabled = false;
            
            thread.Start();
            
        }
        public void serverthread()
        {
            byte[] bytes = new Byte[1024];
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11001);
            listener = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (true)
                {
                    soc = listener.Accept();
                    data = null;
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRead = soc.Receive(bytes);
                        data = data + Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        if (data.EndsWith("<EOF>"))
                        {
                            break;
                        }

                    }
                    data = data.Substring(0, data.Length - 5);
                    if (data == "set")
                    {
                        soc.Send(Encoding.ASCII.GetBytes("ok"));
                        data = "";
                        while (true)
                        {
                            bytes = new byte[1024];
                            int bytesRead = soc.Receive(bytes);
                            data = data + Encoding.ASCII.GetString(bytes, 0, bytesRead);
                            if (data.EndsWith("<EOF>"))
                            {
                                break;
                            }

                        }
                        data = data.Substring(0, data.Length - 5);
                        byte[] msg;
                        if ((msg = Encoding.ASCII.GetBytes(setstockvalue(data))) != null)
                        {
                            soc.Send(msg);
                        }
                    }
                    else
                    {
                        byte[] msg;
                        string val;
                        if ((val = getstockvalue(data)) != "null")
                        {
                            msg = Encoding.ASCII.GetBytes(val);
                        }
                        else
                        {
                            data = "Not-Found"; 
                            msg = Encoding.ASCII.GetBytes(data);
                            //soc.Send(msg);
                        }
                        soc.Send(msg);
                    }

                    soc.Shutdown(SocketShutdown.Both);
                    soc.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public string getstockvalue(string data)
        {
            int i = 0;
            while (Stocklist.Count > i)
            {
                if (Stocklist[i].stockname == data)
                {
                    return Stocklist[i].stockprice.ToString();
                }
                i++;
            }
            return "null";
        }

        public string setstockvalue(string data)
        {
            string [] splt = data.Split(':');
            string sname = splt[0];
            string sval = splt[1];
            int i = 0;
            while (Stocklist.Count > i)
            {
                if (Stocklist[i].stockname == sname)
                {
                    Stocklist[i].stockprice = (float)Convert.ToDouble(sval);
                    return "Updated";
                }
                i++;
            }
            Stock tmp = new Stock();
            tmp.stockname = sname;
            tmp.stockprice = (float)Convert.ToDouble(sval);
            Stocklist.Add(tmp);
            return "Add-New";
            
        }
        
    }

}
