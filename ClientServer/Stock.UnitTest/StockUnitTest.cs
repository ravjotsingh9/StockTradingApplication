using System;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace StockUnitTest
{
    [TestClass]
    public class StockTest
    {

        [TestMethod]
        public void TestMethod1()
        {

            string stockName ="FB";
            Stock test = new Stock();
            string price = test.getPriceFromYahoo(stockName);
            test.addToTheStockList(stockName, price, 100);
         
            Console.WriteLine(test.stocksDictionary[stockName].price);
            test.stocksDictionary[stockName].shares = 700;

            Console.WriteLine(test.stocksDictionary[stockName].shares);
        }
    }
}
