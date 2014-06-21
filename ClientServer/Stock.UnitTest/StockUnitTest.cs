using System;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockUnitTest
{
    [TestClass]
    public class StockTest
    {
        
        [TestMethod]
        public void TestMethod1()
        {
            Stock test = new Stock();
            Console.WriteLine(test.getPriceFromYahoo("FB"));
          
            
        }
    }
}
