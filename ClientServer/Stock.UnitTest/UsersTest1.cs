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
        
    }
}
