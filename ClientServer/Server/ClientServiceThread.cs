using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public partial class ServerMainThread
    {
        string fileNameForStock = "stocks.txt";
        string fileNameForUser = "users.txt ";
        private Object stocksLock = new Object();
        private Object usersLock = new Object();
        //Stock Stocklist;
        //Users userList;
        //public ClientServiceThread()
        //{
        //    Stocklist = new Stock();
        //    userList = new Users();
        //}


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
                lock (stocksLock)
                {
                    Stocklist.updateAllPrice();
                }
                
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
            if ((msg = Encoding.ASCII.GetBytes(data)) != null)
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
                string messagetype = split[0];
                return messagetype;
            }
            else
            {
                return null;
            }
        }



        // return msg format <userName> + ":ok" + ":<EOF>"
         public string responseMsgUSER(string msg)
         {
             if (msg != null)
             {
                 string responseMsg = "";
                // process string format "USER:" + <userName> + ":<EOF>" from client
                 string[] split = msg.Split(':');
                 string userName = split[1];
 
                 if (userList.ifUserExist(userName))
                 {
                     responseMsg = "ok:" + userName  + ":<EOF>";
                     return responseMsg;
                 }
                 else
                 {
                     lock (usersLock)
                     {
                         userList.addUser(userName);
                         userList.writeAllUserData(fileNameForUser);
                     }
                     
                     //responseMsg = userName + " was added into users list" + ":<EOF>";
                     responseMsg = "ok:" + userName + ":<EOF>";
                     return responseMsg;
                 }
             }
             else
             {
                 Console.WriteLine("No user name received.");
                 return null;
             }
         }




         // return msg format <userName> + ":" + <price> + ":<EOF>"
         public string responseMsgQUERY(String msg)
         {

             if (msg != null)
             {

                 string responseMsg = "";

                 // process string format "QUERY:" + <userName> + ":" + <nameOfStock> + ":<EOF>" from client;            
                 string[] split = msg.Split(':');
                 string userName = split[1];
                 string nameOfStock = split[2];
                 string price;

                 // stockList needs to get replaced
                 if (Stocklist.validStockName(nameOfStock))
                 {

                     lock (stocksLock)
                     {
                         // If the stock is not in the tracing list
                         if (!Stocklist.checkInStockDic(nameOfStock))
                         {
                             Stocklist.addToTheStockList(nameOfStock);
                             Stocklist.writeAllStockData(fileNameForStock);
                             
                         }
                         price = Stocklist.stocksDictionary[nameOfStock].price;
                     }    
                     
                     // price is already a string
                     responseMsg = userName + ":" + price + ":<EOF>";
                     return responseMsg;
                 }
                 else
                 {

                     // price is already a string
                     responseMsg = userName + ":" + "Invalid stock name! Please check stock name." + ":<EOF>";
                     return responseMsg;
                 }

             }
             else
             {
                 return null;
             }

         }







         // return msg format "ok:" + <currentBlance> + ":" + <stockName> + ":" + <quantity> ":<EOF>"
         public string responseMsgBUY(String msg)
         {

             if (msg != null)
             {
                 // process string format "BUY:" + <userName> + ":" + <stockName> + ":" + <quantity> + ":<EOF>" from client;            
                 string[] split = msg.Split(':');
                 string userName = split[1];
                 string nameOfStock = split[2];
                 int quantity = Convert.ToInt32(split[3]);
                 string responseMsg = "";
                 string stockMsg = "";
                 bool success = false;
                 double price=0;
                 // check if validStockName
                 if (Stocklist.validStockName(nameOfStock))
                 {
                     lock (stocksLock)
                     {
                         // check if in local stockList
                         if (!Stocklist.checkInStockDic(nameOfStock))
                         {
                             Stocklist.addToTheStockList(nameOfStock);
                         }

                   
                       success =  Stocklist.clientBuyShares(nameOfStock, quantity);
                       price = quantity * Convert.ToDouble(Stocklist.stocksDictionary[nameOfStock].price);

                     }

                     if (success)
                     {
                         lock (usersLock)
                         {
                             if (userList.modifyCash(userName, price, false))
                             {
                                 if (userList.modifyShares(userName, nameOfStock, quantity, true)) ;

                                 string cashBalance = userList.UserDictionary[userName].cashBalance.ToString();

                                 foreach (string key in userList.UserDictionary[userName].StockShares.Keys)
                                 {
                                     stockMsg += ":" + key + ":" + userList.UserDictionary[userName].StockShares[key].ToString();

                                 }
                                 
                                 responseMsg = "ok:" + cashBalance + stockMsg + ":<EOF>";
                             }
                             else
                             {
                                 responseMsg = "Your cash is not enough to buy" + ":<EOF>";
                             }
                           
                         }
                                                
                         return responseMsg;
                     }
                     else
                     {
                         responseMsg = "Stock's shares are not enough to buy" + ":<EOF>";
                         return responseMsg;
                     }

                 }
                 else
                 {
                     responseMsg = "Invalid stock name! Please check stock name." + ":<EOF>";
                     return responseMsg;
                 }

             }
             else
             {
                 return "Cannot process, check again." + ":<EOF>";
             }

         }





         // return msg format "ok:" + <currentBlance> + ":" + <stockName> + ":" + <quantity> ":<EOF>"
         public string responseMsgSELL(String msg)
         {

             if (msg != null)
             {
                 // process string format "SELL:" + <userName> + ":" + <stockName> + ":" + <quantity> + ":<EOF>" from client;            
                 string[] split = msg.Split(':');
                 string userName = split[1];
                 string nameOfStock = split[2];
                 int quantity = Convert.ToInt32(split[3]);

                 string stockMsg = "";
                 string responseMsg = "";

                 // check if stock name is valid
                 // check if user has that particular stock
                 if (Stocklist.validStockName(nameOfStock) &&
                     userList.UserDictionary[userName].StockShares.ContainsKey(nameOfStock))
                 {
                     lock(usersLock)
                     {
                         if (userList.modifyShares(userName, nameOfStock, quantity, false))
                         {

                             double price = quantity * Convert.ToDouble(Stocklist.stocksDictionary[nameOfStock].price);
                             userList.modifyCash(userName, price, true);
                             string cashBalance = userList.UserDictionary[userName].cashBalance.ToString();

                             // generate stockname and stock quantity the user has 
                             foreach (string key in userList.UserDictionary[userName].StockShares.Keys)
                             {
                                 stockMsg += ":" + key + ":" + userList.UserDictionary[userName].StockShares[key].ToString();

                             }
                             responseMsg = "ok:" + cashBalance + stockMsg + ":<EOF>";
                          
                         }
                     }
                     return responseMsg;
               }
                     // check if the user has enough shares to sell

                 else
                 { 
                     responseMsg = "Invalid stock name! Please check stock name." + ":<EOF>";
                     return responseMsg;
                 
                 }
                 


             }

             else
             {
                 return null;
             }

         }



/*

        
         // Get value of stocks [For Assignment Part 2]
          
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


        
         //Set value of stocks [For Assignment Part 2]
          
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
   */
    }
}
