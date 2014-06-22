using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Server.Yahoo_Finance;
//
namespace Server
{
    public class Stock
    {
        public string stockname;
        public float stockprice;

        public class stockInfo
        {
            public string price { get; set; }
            public int shares { get; set; }
        }
        
        // Stock list and price in memory
        //Key:stock' name, value: price, shares
        private Dictionary<string, stockInfo> m_stocksDictionary = new Dictionary<string, stockInfo>();

        public Dictionary<string, stockInfo> stocksDictionary
        {
            get{
                return m_stocksDictionary;
               }
            set
            {
                m_stocksDictionary = value;
            }
        }

        // file's name for stock's information stores in the Disk
        private string fileStock;

        // file's name for users' information stores in the Disk
        private string fileusers;

        // querys to update all the stock's price
        private List<stockQuote> updatequerys { get; set; }
        
        // constructor 
        public Stock()
        {

        }

        ///You can use this function to test the stock value
        ///stockIdentifier : stock's name
        ///For example, Apple should input as AAPL
        ///You can see the right stock name from here:
        ///https://finance.yahoo.com/q?s=AAPL
        // string val = getPriceFromYahoo("AAPL");
        public string getPriceFromYahoo(string stockIdentifier)
        {
            string price = "";
            List<stockQuote> queryPrice = new List<stockQuote>();
            queryPrice.Add(new stockQuote(stockIdentifier));
            if(getStockData.getData(queryPrice))
            {
                stockQuote value =  queryPrice.ElementAt(0);
                price = value.LastTradePrice.ToString();
                return price;
            }

            return null;

        }

        // update the price for all the stocks
        // update information should first write into the disk
        // stocksDictionary should use getStockDataFromFile to update info
        public bool updateAllPrice(Dictionary<string, string> stocksDic)
        {
            bool success = false;
            List<stockQuote> updateQueries = new List<stockQuote>();
            foreach (string key in stocksDic.Keys)
            {
                updateQueries.Add(new stockQuote(key));
            }
            getStockData.getData(updateQueries);

            // write into the file
            writeStockDataInFile(fileStock, updateQueries);
            
            return success;

        }

       
        public void addToTheStockList(string stockName, string price, int shares)
        {
            stockInfo info = new stockInfo();
            info.price = price;
            info.shares = shares;
            this.m_stocksDictionary.Add(stockName, info);
        }

        // wirte stock's information into the disk
        public bool writeStockDataInFile(String FileName, List<stockQuote> data){
            bool success = false;
            return success;
        }

        // get stock's information from the disk
        public bool getStockDataFromFile(String FileName)
        {
            bool success = false;
            return success;
        }




        //check if stock already in the tracing list
        //Server can call this function to check
        public bool checkInStockList(string name)
        {
            return stocksDictionary.ContainsKey(name);
        }

        //check if the user enter the correct stock name
        //UI can call this function to display error to the user
        public bool validStockName(string name)
        {

            if (getPriceFromYahoo(name) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
