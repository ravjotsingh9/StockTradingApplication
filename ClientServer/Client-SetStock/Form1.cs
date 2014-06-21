using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_SetStock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(txtname.Text == "" || txtvalue.Text == "")
            {
                MessageBox.Show("Name and Value cannot be empty.", "Please prove name and value");
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

                    byte[] msg_set_req = Encoding.ASCII.GetBytes("set" + "<EOF>");
                    int bytesSent_req = snder.Send(msg_set_req);
                    int bytesRec_req = snder.Receive(bytes);
                    string resp = Encoding.ASCII.GetString(bytes, 0, bytesRec_req);
                    if (resp == "ok")
                    {
                        resp = "";
                        byte[] msg = Encoding.ASCII.GetBytes(txtname.Text +":"+ txtvalue.Text + "<EOF>");
                        int bytesSent = snder.Send(msg);
                        int bytesRec = snder.Receive(bytes);
                        resp = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        label3.Text = resp;
                    }
                    /*
                    txtvalue.Text = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                     */ 
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
