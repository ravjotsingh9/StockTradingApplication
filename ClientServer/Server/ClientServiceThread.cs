using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ClientServiceThread
    {
        Stock Stocklist;
        Users userList;
        

        public ClientServiceThread()
        {
            Stocklist = new Stock();
            userList = new Users();
        }


        public void startServicingClient(Socket soc)
        {
            ThreadPool.QueueUserWorkItem(servicethread, (Object)soc);
        }

        /*
         * Stock watchdog thread start function
         */ 
        public void stockwatchdog()
        {
            while(true)
            {
                Stocklist.updateAllPrice();
                Thread.Sleep(120000);
            }
        }

        /*
         * Thread start-function to service single request from client
         */ 
        private void servicethread(Object socket)
        {
            Socket soc = (Socket)socket;
            string data = readdata(soc);
            string msgtype = findMsgtype(data);
            if(msgtype == "USER" || msgtype == "user")
            {
                string response ="";
                if((response= responseMsgUSER(data))!= null)
                {
                    writedata(soc, response);
                }
            }
            else
            {
                if (msgtype == "QUERY" || msgtype == "query")
                {
                    string response = "";
                    if ((response = responseMsgQUERY(data)) != null)
                    {
                        writedata(soc, response);
                    }
                }
                else
                {
                    if (msgtype == "BUY" || msgtype == "buy")
                    {
                        string response = "";
                        if ((response = responseMsgBUY(data)) != null)
                        {
                            writedata(soc, response);
                        }
                    }
                    else
                    {
                        if (msgtype == "SELL" || msgtype == "sell")
                        {
                            string response = "";
                            if ((response = responseMsgSELL(data)) != null)
                            {
                                writedata(soc, response);
                            }
                        }
                    }
                }
            }
            /*
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
            */
            soc.Shutdown(SocketShutdown.Both);
            soc.Close();
        }


        /*
         * Read Data from Socket until <EOF> is received
         */ 
        private string readdata(Socket soc)
        {
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
            return data;
        }


        /*
         * To write data on the socket
         */ 
        private bool writedata(Socket soc, string data)
        {
            byte[] msg;
            if ((msg = Encoding.ASCII.GetBytes(setstockvalue(data))) != null)
            {
                soc.Send(msg);
                return true;
            }
            else
            {
                return false;
            }
        }


        /*
         * To find out the type of message is
         */
        private string findMsgtype(string msg)
        {
            if (msg != null)
            {
                string[] split = msg.Split(':');
                string messagetype = split[1];
                return messagetype;
            }
            else
            {
                return null;
            }
        }


         private string responseMsgUSER(string msg)
         {
             if (msg != null)
             {
                 string responseMsg = "";
                // process string format "USER:" + <userName> + ":<EOF>" from client
                 string[] split = msg.Split(':');
                 string userName = split[1];
 
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


        /*
         * Get value of stocks [For Assignment Part 2]
         */ 
        private string getstockvalue(string data)
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


        /*
         * Set value of stocks [For Assignment Part 2]
         */ 
        private string setstockvalue(string data)
        {
            string[] splt = data.Split(':');
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

    }
}
