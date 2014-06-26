using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections.ObjectModel;
using Server.Yahoo_Finance;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]

        // test responseMsgUSER of server side
        public void testResponseMsgUSER()
        {
            Server.Form1 testForm1 = new Server.Form1();
            Users userList = testForm1.userList;

            userList.addUser("Abyss");
            userList.addStcokForUser("Abyss", "abc", 100);


            string msg = testForm1.responseMsgUSER("USER:" + "Abyss" + ":<EOF>");

            Console.WriteLine(msg);
        }


        [TestMethod]
        // test responseMsgQUERY of server side
        public void testInterpretRate()
        {

            Server.Form1 testForm1 = new Server.Form1();
            Users userList = testForm1.userList;
            userList.addUser("Abyss");
        
            string msg = testForm1.responseMsgQUERY("QUERY:Abyss:AATTTL:<EOF>");
            Console.WriteLine(msg);
        }


        [TestMethod]
        // test responseMsgBUY of server side
        public void testResponseMsgBUY()
        {

            Server.Form1 testForm1 = new Server.Form1();
            Users userList = testForm1.userList;
            userList.addUser("Abyss");
            userList.addStcokForUser("Abyss", "AAPL", 100);
            userList.addStcokForUser("Abyss", "FB", 200);
            // "BUY:" + <userName> + ":" + <stockName> + ":" + <quantity> + ":<EOF>" from client;            
            string msg = testForm1.responseMsgBUY("BUY:Abyss:AAPL:1:<EOF>");


            Console.WriteLine(msg);
        }


        [TestMethod]
        // test responseMsgSELL of server side
        public void testResponseMsgSELL()
        {

            Server.Form1 testForm1 = new Server.Form1();
            Users userList = testForm1.userList;
            userList.addUser("Abyss");
            userList.addStcokForUser("Abyss", "AAPL", 100);
            userList.addStcokForUser("Abyss", "FB", 200);
            // "SELL:" + <userName> + ":" + <stockName> + ":" + <quantity> + ":<EOF>" from client;            
            string msg = testForm1.responseMsgSELL("SELL:Abyss:AAPL:1:<EOF>");
            
            Console.WriteLine(msg);
        }

    }
}
