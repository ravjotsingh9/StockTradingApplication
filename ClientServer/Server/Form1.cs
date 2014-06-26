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
        
        List<Stock> Stocklist;
        volatile bool stop;
        Thread thread;

        public Form1()
        {
            thread = new Thread(new ThreadStart(serverthread));
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
                    ThreadPool.QueueUserWorkItem(servicethread, (Object)soc);

                    /*
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
                     */ 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            thread.Abort();
           
        }

        private void servicethread(Object socket)
        {
            Socket soc = (Socket)socket;
            string data = null;
            byte[] bytes;
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
            //Thread.Sleep(5000);
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


        
        private void button2_Click(object sender, EventArgs e)
        {
            if (thread.IsAlive == true)
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

                //button2.Enabled = false;
                button1.Enabled = true;
            }  
            else

            {
                Application.Exit();
            }
        }



        public string interpretUsername(String msg)
        {
            if (msg != null)
            {
                string responseMsg = "";
                // process string format "USER:" + <userName> + ":<EOF>" from client
                string[] split = msg.Split(':');
                string userName = split[1];

 
                // userList is the Users ojbect, should be placed
                Users userList = new Users();
                if (userList.UserDictionary.ContainsKey(userName))
                {

                    responseMsg = userName + ":ok" + ":<EOF>";
                    return responseMsg;
                }
                else
                {
                    responseMsg = userName + "User doesn't exist!" + ":<EOF>";
                    return responseMsg;
                }
            }
            else
            {
                Console.WriteLine("No user name received.");
                return null;
            }

        }
        
    }

}
