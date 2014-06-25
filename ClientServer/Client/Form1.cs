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
            if(txtStockname.Text  == "")
            {
                MessageBox.Show("Stock Name cannot be empty.","Please provide the stock name");
                return;
            }
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress,11001);

                // Create a TCP/IP  socket.
                Socket snder = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

                // Connect the socket to the remote endpoint. Catch any errors.
                try 
                {
                    snder.Connect(remoteEP);
                    byte[] msg = Encoding.ASCII.GetBytes(txtStockname.Text +"<EOF>");
                    int bytesSent = snder.Send(msg);
                    int bytesRec = snder.Receive(bytes);
                    lblrate.Text = Encoding.ASCII.GetString(bytes, 0, bytesRec);
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

       
        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }
    }
}
