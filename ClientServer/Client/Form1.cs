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

namespace Client
{
    public partial class Form1 : Form
    {
        string userName;
        public Form1()
        {
            userName = "";
            InitializeComponent();
            grptrading.Enabled = false;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //lblrate.Text = "Processing...";
            if(userName== "" )
            {
                //lblrate.Text = "<Processing Status>";
                MessageBox.Show("No user name selected.","Please Login first");
                return;
            }
            else
            {
                if(txtStockname.Text  == "")
                {
                    //lblrate.Text = "<Processing Status>";
                    MessageBox.Show("Stock name cannot be empty.", "Please provide them");
                    return;
                }
                else
                {
                    
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
                            byte[] msg = Encoding.ASCII.GetBytes("QUERY:"+txtuser.Text + ":" + txtStockname.Text + ":<EOF>");
                            int bytesSent = snder.Send(msg);
                            int bytesRec = snder.Receive(bytes);
                            String Ratereponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            //lblrate.Text = "<Processing Status>";
                            string[] resp = Ratereponse.Split(':');
                            MessageBox.Show(resp[1],"Response");
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
            }
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
        
        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (userName == "")
            {
                MessageBox.Show("No user name selected.", "Please Login first");
                return;
            }
            else
            {
                if(txtStockname.Text=="")
                {
                    MessageBox.Show("Stock Name cannot be empty.", "Please provide your the Stock name");
                    return;
                }
                    else if(txtQnty.Text=="")
                {
                    MessageBox.Show("Stock Quantity cannot be empty.", "Please provide your the Stock Quantity");
                    return;
                }
                else
                {
                    double Num;
                    bool isNum = double.TryParse(txtQnty.Text, out Num);
                    if (isNum)
                    {
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
                                MessageBox.Show(resp[0],"Response");
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
                    else
                    {
                        MessageBox.Show("Quantity should be integers and nothing else");
                    }
                }
            }
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

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                MessageBox.Show("User Name cannot be empty.", "Please provide your user name and login first");
                return;
            }
            else
            {
                if (txtStockname.Text == "")
                {
                    MessageBox.Show("Stock Name cannot be empty.", "Please provide your the Stock name");
                    return;
                }
                else if (txtQnty.Text == "")
                {
                    MessageBox.Show("Stock Quantity cannot be empty.", "Please provide your the Stock Quantity");
                    return;
                }
                else
                {
                    double Num;
                    bool isNum = double.TryParse(txtQnty.Text, out Num);
                    if (isNum)
                    {
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
                                MessageBox.Show(resp[0], "Response");
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
                    else
                    {
                        MessageBox.Show("Quantity should be integers and nothing else");
                    }
                }
            }
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