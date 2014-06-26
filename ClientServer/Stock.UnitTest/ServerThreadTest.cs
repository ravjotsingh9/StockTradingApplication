using System;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ServerThreadTest
    {

        ServerMainThread test = new ServerMainThread();
        [TestMethod]
        public void rMUserTest()
        {
            Console.WriteLine(test.responseMsgUSER("user:userOne"));
            foreach (string key in test.userList.UserDictionary.Keys)
            {
                Console.WriteLine(key + test.userList.UserDictionary[key].cashBalance);
            }
        }
        
        [TestMethod]
        public void responseMsgQUERYTest()
        {
            Console.WriteLine( test.responseMsgQUERY("uu:" + "userOne:" + "FB"));
        }

        [TestMethod]
        public void rresponseMsgBUYTest()
        {
            test.responseMsgUSER("user:userOne");
            Console.WriteLine(test.responseMsgBUY("uu:" + "userOne:" + "FB:"+"10"));
            
        }

        [TestMethod]
        public void rresponseMsgSellTest()
        {
            test.responseMsgUSER("user:userOne");
            Console.WriteLine(test.responseMsgBUY("uu:" + "userOne:" + "FB:" + "10"));
            Console.WriteLine(test.responseMsgSELL("uu:" + "userOne:" + "FB:" + "5"));
        }

        [TestMethod]
        public  void mainThreadTest()
        {

            Form1 yyy = new Form1();
          
            
        }
    }
}
