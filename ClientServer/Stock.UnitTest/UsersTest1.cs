using System;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UsersTest1
    {
        [TestMethod]
        public void AddTestMethod()
        {
            Users test = new Users();
            test.addUser("hello");
            Console.WriteLine("User:hello's balance: {0}", test.UserDictionary["hello"].cashBalance);
            test.modifyCash("hello", 100, true);
            Console.WriteLine("User:hello's balance: {0}", test.UserDictionary["hello"].cashBalance);
            
            if (test.UserDictionary.ContainsKey("hello"))
            {
                if (test.addStcokForUser("hello", "AAPL", 100))
                {
                    Console.WriteLine("123");
                    Console.WriteLine("User:hello's STOCK: {0}", test.UserDictionary["hello"].StockShares["AAPL"]);
                }
                
            }
           
     
        }

        [TestMethod]
        public void UsersMethod()
        {
            Users test = new Users();
            test.addUser("hello");
            Console.WriteLine("User:hello's balance: {0}", test.UserDictionary["hello"].cashBalance);
            test.modifyCash("hello", 100, true);
            Console.WriteLine("User:hello's balance: {0}", test.UserDictionary["hello"].cashBalance);

            if (test.UserDictionary.ContainsKey("hello"))
            {
                if (test.addStcokForUser("hello", "AAPL", 100))
                {
                    Console.WriteLine("123");
                    Console.WriteLine("User:hello's STOCK: {0}", test.UserDictionary["hello"].StockShares["AAPL"]);
                }

            }

            test.addStcokForUser("hello", "FB", 500);
            
            test.addUser("second");
            test.addStcokForUser("second", "AAPL", 400);
            test.writeAllUserData("ttt.txt");


        }

        [TestMethod]
        public void getUserTest()
        {
            Users test = new Users();
            test.getUserDataFromFile("ttt.txt");
            foreach(string key in test.UserDictionary.Keys){
                Console.WriteLine("{0} {1}", key, test.UserDictionary[key].cashBalance);
                foreach (string key2 in test.UserDictionary[key].StockShares.Keys)
                {
                    Console.WriteLine("{0} {1}", key2, test.UserDictionary[key].StockShares[key2]);
                }
                
            }
            

        }


        [TestMethod]
        public void writeSingleUserTest()
        {
            Users test = new Users();
            test.addUser("third");
            test.addStcokForUser("third","AAPL",400);
            test.addStcokForUser("third", "FB", 700);
            test.writeSingleUserData("ttt.txt", "third");

        }
        
    }
}
