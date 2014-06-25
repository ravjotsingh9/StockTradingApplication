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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtuser.Text== "" || lblusernameResponse.Text!= "0k")
            {
                MessageBox.Show("User name cannot be empty.","Please provide them and Login first");
                return;
            }
            else
            {
                if(txtStockname.Text  == "")
                {
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
                            byte[] msg = Encoding.ASCII.GetBytes("QUERY:"+txtuser.Text + ":" + txtStockname.Text + "<EOF>");
                            int bytesSent = snder.Send(msg);
                            int bytesRec = snder.Receive(bytes);
                            String Ratereponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            lblrate.Text = interpretRate(Ratereponse);
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

        private void btnLogin_Click_1(object sender, EventArgs e)
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
                    byte[] msg = Encoding.ASCII.GetBytes("USER:"+txtStockname.Text + "<EOF>");
                    int bytesSent = snder.Send(msg);
                    int bytesRec = snder.Receive(bytes);
                    string loginresp = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    lblusernameResponse.Text=interpretUsername(loginresp);
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
                msg = Userresponse[1];
            }
            else
            {
                msg = "No Response";
            }
            return msg;
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

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || lblusernameResponse.Text != "0k")
            {
                MessageBox.Show("User Name cannot be empty.", "Please provide your user name and login first");
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
                                byte[] msg = Encoding.ASCII.GetBytes("BUY:" + txtuser + ":" + txtStockname.Text + ":" + txtQnty.Text + "<EOF>");
                                int bytesSent = snder.Send(msg);
                                int bytesRec = snder.Receive(bytes);
                                string Buyresponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                lblrate.Text = interpretBuy(Buyresponse);
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
        }

        private string interpretBuy(string msg)
        {
            string BuyInfo = null;
            if(msg!=null)
            {
                string[] response = Regex.Split(msg, ":");
                if (response[0] == "ok")
                {
                    for (int i = 1; i < response.Length; i++)
                    {
                        BuyInfo = BuyInfo +" "+ response[i];
                    }
                }
                else
                {
                    BuyInfo = "Your request cannot be processed because: ";
                    for (int i = 1; i < response.Length; i++)
                    {
                        BuyInfo = BuyInfo + " " + response[i];
                    }
                }
            }
            return BuyInfo;
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            if (txtuser.Text == "" || lblusernameResponse.Text != "0k")
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
                                byte[] msg = Encoding.ASCII.GetBytes("SELL:" + txtuser + ":" + txtStockname.Text + ":" + txtQnty.Text + "<EOF>");
                                int bytesSent = snder.Send(msg);
                                int bytesRec = snder.Receive(bytes);
                                string Sellresponse = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                lblrate.Text = interpretSell(Sellresponse);
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
                if (response[0] == "ok")
                {
                    for (int i = 1; i < response.Length; i++)
                    {
                        SellInfo = SellInfo + " " + response[i];
                    }
                }
                else
                {
                    SellInfo = "Your request cannot be processed because: ";
                    for (int i = 1; i < response.Length; i++)
                    {
                        SellInfo = SellInfo + " " + response[i];
                    }
                }
            }
            return msg;
        }
    }
}