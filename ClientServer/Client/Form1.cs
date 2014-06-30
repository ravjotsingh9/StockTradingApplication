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
using System.Text.RegularExpressions;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        string userName;
        Thread updatebtn;
        Thread Querybtnclickthread;
        Thread Buybtnclickthread;
        Thread Sellbtnclickthread;
        delegate void updatebtnstatus(string str);
        public Form1()
        {
            userName = "";
            InitializeComponent();
            grptrading.Enabled = false;
            //bool str1 = false;
            updatebtn = new Thread(() => updatethread("q:f"));
            //updatebtn.Start();
        }

        private void updatethread(string btn)
        {
            //updatestatus(btn);
            updatebtnstatus update = new updatebtnstatus(updatestatus);
            this.Invoke(update, btn);
            updatebtn.Abort();
        }
        private void updatestatus(string str)
        {
            string[] arg = str.Split(':');
            string btn = arg[0];
            string processing = arg[1];
            if (btn.Equals("q"))
            {
                //if (this.Query.InvokeRequired)
                //{
                //    updatebtnstatus update = new updatebtnstatus(updatestatus);
                //    this.Invoke(update);
                //}
                //else
                //{
                    if (processing.Equals("t"))
                    {
                        Query.Text = "Processing...";
                        Query.Enabled = false;
                        //lblrate.Text = str;
                    }
                    else
                    {
                        Query.Text = "Query";
                        Query.Enabled = true;
                    }
                //}
            }
            else
            {
                if (btn.Equals("b"))
                {
                    //if (this.btnBuy.InvokeRequired)
                    //{
                    //    updatebtnstatus update = new updatebtnstatus(updatestatus);
                    //    this.Invoke(update);
                    //}
                    //else
                    //{
                        if (processing.Equals("t"))
                        {
                            btnBuy.Text = "Processing...";
                            btnBuy.Enabled = false;
                            //lblrate.Text = str;
                        }
                        else
                        {
                            btnBuy.Text = "Buy";
                            btnBuy.Enabled = true;
                        }
                    //}
                }
                else
                {
                    if (btn.Equals("s"))
                    {
                        //if (this.btnSell.InvokeRequired)
                        //{
                        //    updatebtnstatus update = new updatebtnstatus(updatestatus);
                        //    this.Invoke(update);
                        //}
                        //else
                        //{
                            if (processing.Equals("t"))
                            {
                                btnSell.Text = "Processing...";
                                btnSell.Enabled = false;
                                //lblrate.Text = str;
                            }
                            else
                            {
                                btnSell.Text = "Sell";
                                btnSell.Enabled = true;
                            }
                        //}
                    }
                }
            }
        }

        private void QuerybtnClickedstart()
        {
            if (userName == "")
            {
                //lblrate.Text = "<Processing Status>";
                MessageBox.Show("No user name selected.", "Please Login first");
                Querybtnclickthread.Abort();
                return;
            }
            else
            {
                if (txtStockname.Text == "")
                {
                    //lblrate.Text = "<Processing Status>";
                    MessageBox.Show("Stock name cannot be empty.", "Please provide them");
                    Querybtnclickthread.Abort();
                    return;
                }
                else
                {
                    // TO DISPLAY STATUS
                    string str = "q:t";
                    updatebtn = new Thread(() => updatethread(str));
                    updatebtn.Start();
                    // Data buffer for incoming data.
                    byte[] bytes = new byte[1024];

                    // Connect to a remote device.
                    try
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
                            snder.Connect(remoteEP);
                            byte[] msg = Encoding.ASCII.GetBytes("QUERY:" + txtuser.Text + ":" + txtStockname.Text + ":<EOF>");
                            int bytesSent = snder.Send(msg);
                            int bytesRec = snder.Receive(bytes);
                            String Ratereponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            //lblrate.Text = "<Processing Status>";
                            string[] resp = Ratereponse.Split(':');
                            // TO DISPLAY STATUS
                            updatebtn = new Thread(() => updatethread("q:f"));
                            updatebtn.Start();
                            MessageBox.Show(resp[1], "Response");
                            snder.Shutdown(SocketShutdown.Both);
                            snder.Close();
                        }
                        catch (ArgumentNullException ane)
                        {
                            updatebtn = new Thread(() => updatethread("q:f"));
                            updatebtn.Start();
                            MessageBox.Show(ane.ToString());
                            Querybtnclickthread.Abort();
                        }
                        catch (SocketException se)
                        {
                            updatebtn = new Thread(() => updatethread("q:f"));
                            updatebtn.Start();
                            MessageBox.Show(se.ToString());
                            Querybtnclickthread.Abort();
                        }
                        catch (Exception ex)
                        {
                            updatebtn = new Thread(() => updatethread("q:f"));
                            updatebtn.Start();
                            MessageBox.Show(ex.ToString());
                            Querybtnclickthread.Abort();
                        }

                    }
                    catch (Exception exx)
                    {
                        MessageBox.Show(exx.ToString());
                        Querybtnclickthread.Abort();
                    }
                }
            }
            Querybtnclickthread.Abort();
        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            Querybtnclickthread = new Thread(new ThreadStart(QuerybtnClickedstart));
            Querybtnclickthread.Start();
        }

        private string interpretRate(String msg)
        {
            if (msg != null)
            {
                String[] Rateresponse = Regex.Split(msg, ":");
                    msg = Rateresponse[1];
            }
            else
            {
                msg = "No Response";
            }
            return msg;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtuser.Text;
            if(username == "" )
            {
                MessageBox.Show("User Name cannot be empty.", "Please provide your user name");
                return;
            }

            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
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
                    snder.Connect(remoteEP);
                    byte[] msg = Encoding.ASCII.GetBytes("USER:"+txtuser.Text + ":<EOF>");
                    int bytesSent = snder.Send(msg);
                    int bytesRec = snder.Receive(bytes);
                    string loginresp = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (interpretUsername(loginresp)=="ok")
                    {
                        userName = txtuser.Text;
                        grptrading.Enabled = true;
                        lblusernameResponse.Text = "Login";
                    }
                    snder.Shutdown(SocketShutdown.Both);
                    snder.Close();

                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(ane.ToString());
                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.ToString());
            }

        }

        private string interpretUsername(String msg)
        {
            if(msg!=null)
            {
                String[] Userresponse = Regex.Split(msg, ":");
                msg = Userresponse[0] ;
            }
            else
            {
                msg = "No Response";
            }
            return msg;
        }
        
        private void BuybtnStart()
        {
            if (userName == "")
            {
                MessageBox.Show("No user name selected.", "Please Login first");
                Buybtnclickthread.Abort();
                return;
            }
            else
            {
                if (txtStockname.Text == "")
                {
                    MessageBox.Show("Stock Name cannot be empty.", "Please provide your the Stock name");
                    Buybtnclickthread.Abort();
                    return;
                }
                else if (txtQnty.Text == "")
                {
                    MessageBox.Show("Stock Quantity cannot be empty.", "Please provide your the Stock Quantity");
                    Buybtnclickthread.Abort();
                    return;
                }
                else
                {
                    double Num;
                    bool isNum = double.TryParse(txtQnty.Text, out Num);
                    if (isNum)
                    {
                        // TO DISPLAY STATUS
                        updatebtn = new Thread(() => updatethread("b:t"));
                        updatebtn.Start();

                        byte[] bytes = new byte[1024];

                        // Connect to a remote device.
                        try
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
                                snder.Connect(remoteEP);
                                //lblrate.Text = "Processing...";
                                byte[] msg = Encoding.ASCII.GetBytes("BUY:" + txtuser.Text + ":" + txtStockname.Text + ":" + txtQnty.Text + ":<EOF>");
                                int bytesSent = snder.Send(msg);
                                int bytesRec = snder.Receive(bytes);
                                string Buyresponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                //lblrate.Text = "";
                                string[] resp = Buyresponse.Split(':');
                                // TO DISPLAY STATUS
                                updatebtn = new Thread(() => updatethread("b:f"));
                                updatebtn.Start();
                                MessageBox.Show(resp[0], "Response");
                                snder.Shutdown(SocketShutdown.Both);
                                snder.Close();

                            }
                            catch (ArgumentNullException ane)
                            {
                                updatebtn = new Thread(() => updatethread("b:f"));
                                updatebtn.Start();
                                MessageBox.Show(ane.ToString());
                                Buybtnclickthread.Abort();
                            }
                            catch (SocketException se)
                            {
                                updatebtn = new Thread(() => updatethread("b:f"));
                                updatebtn.Start();
                                MessageBox.Show(se.ToString());
                                Buybtnclickthread.Abort();
                            }
                            catch (Exception ex)
                            {
                                updatebtn = new Thread(() => updatethread("b:f"));
                                updatebtn.Start();
                                MessageBox.Show(ex.ToString());
                                Buybtnclickthread.Abort();
                            }

                        }
                        catch (Exception exx)
                        {
                            updatebtn = new Thread(() => updatethread("b:f"));
                            updatebtn.Start();

                            MessageBox.Show(exx.ToString());
                            Buybtnclickthread.Abort();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Quantity should be integers and nothing else");
                        Buybtnclickthread.Abort();
                    }
                }
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            Buybtnclickthread = new Thread(new ThreadStart(BuybtnStart));
            Buybtnclickthread.Start();
        }

        private string interpretBuy(string msg)
        {
            string BuyInfo = null;
            if(msg!=null)
            {
                string[] response = Regex.Split(msg, ":");
                if (response[0].ToLower() == "ok")
                {
                    for (int i = 1; i < response.Length; i++)
                    {
                        if (response[i] != "<EOF>")
                        {
                            BuyInfo = BuyInfo + " " + response[i];
                        }
                    }
                }
                else
                {
                    BuyInfo = "Your request cannot be processed because: ";
                    for (int i = 1; i < response.Length; i++)
                    {
                        if (response[i] != "<EOF>")
                        {
                            BuyInfo = BuyInfo + " " + response[i];
                        }
                    }
                }
            }
            return BuyInfo;
        }

        private void SellbtnStart()
        {
            if (txtuser.Text == "")
            {
                MessageBox.Show("User Name cannot be empty.", "Please provide your user name and login first");
                Sellbtnclickthread.Abort();
                return;
            }
            else
            {
                if (txtStockname.Text == "")
                {
                    MessageBox.Show("Stock Name cannot be empty.", "Please provide your the Stock name");
                    Sellbtnclickthread.Abort();
                    return;
                }
                else if (txtQnty.Text == "")
                {
                    MessageBox.Show("Stock Quantity cannot be empty.", "Please provide your the Stock Quantity");
                    Sellbtnclickthread.Abort();
                    return;
                }
                else
                {
                    double Num;
                    bool isNum = double.TryParse(txtQnty.Text, out Num);
                    if (isNum)
                    {
                        // TO DISPLAY STATUS
                        updatebtn = new Thread(() => updatethread("s:t"));
                        updatebtn.Start();
                        byte[] bytes = new byte[1024];

                        // Connect to a remote device.
                        try
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
                                snder.Connect(remoteEP);
                                //lblrate.Text = "Processing...";
                                byte[] msg = Encoding.ASCII.GetBytes("SELL:" + txtuser.Text + ":" + txtStockname.Text + ":" + txtQnty.Text + ":<EOF>");
                                int bytesSent = snder.Send(msg);
                                int bytesRec = snder.Receive(bytes);
                                string Sellresponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                //lblrate.Text = "";
                                string[] resp = Sellresponse.Split(':');
                                // TO DISPLAY STATUS
                                updatebtn = new Thread(() => updatethread("s:f"));
                                updatebtn.Start();
                                MessageBox.Show(resp[0], "Response");
                                snder.Shutdown(SocketShutdown.Both);
                                snder.Close();

                            }
                            catch (ArgumentNullException ane)
                            {
                                updatebtn = new Thread(() => updatethread("s:f"));
                                updatebtn.Start();
                                MessageBox.Show(ane.ToString());
                                Sellbtnclickthread.Abort();
                            }
                            catch (SocketException se)
                            {
                                updatebtn = new Thread(() => updatethread("s:f"));
                                updatebtn.Start();
                                MessageBox.Show(se.ToString());
                                Sellbtnclickthread.Abort();
                            }
                            catch (Exception ex)
                            {
                                updatebtn = new Thread(() => updatethread("s:f"));
                                updatebtn.Start();
                                MessageBox.Show(ex.ToString());
                                Sellbtnclickthread.Abort();
                            }

                        }
                        catch (Exception exx)
                        {
                            updatebtn = new Thread(() => updatethread("s:f"));
                            updatebtn.Start();
                            MessageBox.Show(exx.ToString());
                            Sellbtnclickthread.Abort();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Quantity should be integers and nothing else");
                        Sellbtnclickthread.Abort();
                    }
                }
            }
        }


        private void btnSell_Click(object sender, EventArgs e)
        {
            Sellbtnclickthread = new Thread(new ThreadStart(SellbtnStart));
            Sellbtnclickthread.Start();
        }
        
        private string interpretSell(string msg)
        {
            string SellInfo = null;
            if(msg!=null)
            {
                string[] response = Regex.Split(msg, ":");
                if (response[0].ToLower() == "ok")
                {
                    for (int i = 1; i < response.Length; i++)
                    {
                        if (response[i] != "<EOF>")
                        {
                            SellInfo = SellInfo + " " + response[i];
                        }
                    }
                }
                else
                {
                    SellInfo = "Your request cannot be processed because: ";
                    for (int i = 1; i < response.Length; i++)
                    {
                        if (response[i] != "<EOF>")
                        {
                            SellInfo = SellInfo + " " + response[i];
                        }
                    }
                }
            }
            return msg;
        }
    }
}