﻿using System;
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
        
        /*    
        byte[] bytes;
            IPHostEntry ipHostInfo;
            IPAddress ipAddress;
            IPEndPoint localEndPoint;
          */  
        string data;
        
        List<Stock> Stocklist;
        volatile bool stop;
        Thread thread;
        public Form1()
        {
            Stocklist  = new List<Stock>();
            InitializeComponent();
            Stock obj = new Stock();
            obj.stockname = "abc";
            obj.stockprice = 10;
            Stocklist.Add(obj);
            //button2.Enabled = false;
            stop = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(serverthread));
            label1.Text = "Running";
            button1.Enabled = false;
            //button2.Enabled = true;
            thread.Start();
        }
        public void serverthread()
        {

            byte [] bytes = new byte[1024];
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11001);
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            try
            {
                // Start listening for connections.
                while (true && !stop)
                {
                    Socket soc = listener.Accept();
                    if (stop)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Do you really want to shut down server? ", "Allow", MessageBoxButtons.YesNo))
                        {
                         
                            soc.Shutdown(SocketShutdown.Both);
                            soc.Close();
                            Application.Exit();
                            break;
                        }
                    }
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
                        ///You can tempory use the following to test the stock value
                        ///In the Client send, please input the correct stock name
                        ///For example, Apple should input as AAPL
                        ///You can see the right stock name from here:
                        ///https://finance.yahoo.com/q?s=AAPL
                        //val = getPriceFromYahoo(data);
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
            
            thread.Abort();
            
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
            return "Added-New";
            
        }

        private ObservableCollection<stockQuote> query { get; set; }
        
        private String getPriceFromYahoo(String stockIdentifier){
            string price="";
            query = new ObservableCollection<stockQuote>();
            query.Add(new stockQuote(stockIdentifier));
            stockQuote rr = getStockData.getData(query);
            price = rr.LastTradePrice.ToString();
            return price;
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11001);

                // Create a TCP/IP  socket.
                Socket snder = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    stop = true;
                    snder.Connect(remoteEP);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); 
                }
                snder.Shutdown(SocketShutdown.Both);    
            snder.Close();
            
                button2.Enabled = false;
                button1.Enabled = true;
                
        }

                
    }

}
