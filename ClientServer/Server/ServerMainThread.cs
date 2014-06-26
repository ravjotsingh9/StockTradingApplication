﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    class ServerMainThread
    {
        
        volatile bool stop;
        Thread thread;
        ClientServiceThread serviceClient;

        public ServerMainThread()
        {
            thread = new Thread(new ThreadStart(serverthread));
            serviceClient = new ClientServiceThread();
            stop = false;
        }
        /*
         * Start the server thread
         */ 
        public void startMainThread()
        {
            thread.Start();
        }


        /*
         * Stops the server thread
         */ 
        public void stopMainThread()
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
            }
            else
            {
                Application.Exit();
            }
        }
        
        
        /* 
         * Server Main thread start function
         */ 
        private void serverthread()
        {
            byte[] bytes = new byte[1024];
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
                    serviceClient.startServicingClient(soc);            

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
    }
}
