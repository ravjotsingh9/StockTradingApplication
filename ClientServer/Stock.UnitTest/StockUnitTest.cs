using System;
using Server.Yahoo_Finance;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace StockUnitTest
{
    [TestClass]
    public class StockTest
    {
        
        /**[TestMethod]
        public void TestMethod1()
        {

            string stockName ="FB";
            Stock test = new Stock();
            string price = test.getPriceFromYahoo(stockName);
            test.addToTheStockList(stockName, price, 100);
         
            //Console.WriteLine(test.stocksDictionary[stockName].price);
            //test.stocksDictionary[stockName].shares = 700;

            //Console.WriteLine(test.stocksDictionary[stockName].shares);
        }**/

        // Test for non-exist data
        [TestMethod]
        public void getDataTest()
        {
            string stockName = "nono";
            Stock test = new Stock();
            string price = test.getPriceFromYahoo(stockName);
            if(price==null)
                Console.WriteLine("is null");
            
        }

        // Test for non-exist data
        [TestMethod]
        public void validStockNameTest()
        {
            string stockName = "nono";
            Stock test = new Stock();
            
            if (!test.validStockName(stockName))
                Console.WriteLine("stock does not exist");

        }

        // Test for write file
        [TestMethod]
        public void writeStockDataInFileTest()
        {
            Stock test = new Stock();
            test.writeNewStockData("test.txt", "FB");

        }

        // Test for get file
        [TestMethod]
        public void getStockDataInFileTest()
        {
            Stock test = new Stock();
            test.getStockDataFromFile("test.txt");
            
            foreach(String key in test.stocksDictionary.Keys){
                Console.WriteLine("{0} {1} {2}", key, test.stocksDictionary[key].price, test.stocksDictionary[key].shares);
            }

            if (test.updateAllPrice())
            {           
                test.writeAllStockData("test.txt");
            }
            else
            {
                Console.WriteLine("false");
            }
            
        }
    }
}
