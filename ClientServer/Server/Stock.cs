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
        
        // Stock list and price in memory
        //Key:stock' name, value: price, shares
        private Dictionary<string, Tuple<string, int>> m_stocksDictionary = new Dictionary<string, Tuple<string, int>>();

        public Dictionary<string, Tuple<string, int>> stocksDictionary
        {
            get{
                return m_stocksDictionary;
               }
        }

        // file's name for stock's information stores in the Disk
        private string fileStock;

        // file's name for users' information stores in the Disk
        private string fileusers;

        // querys to update all the stock's price
        private List<stockQuote> updatequerys { get; set; }

        //stocks' share number
        private int shares;
        
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
        public String getPriceFromYahoo(String stockIdentifier)
        {
            string price = "";
            List<stockQuote> queryPrice = new List<stockQuote>();
            queryPrice.Add(new stockQuote(stockIdentifier));
            getStockData.getData(queryPrice);
            stockQuote value =  queryPrice.ElementAt(0);
            price = value.LastTradePrice.ToString();
            return price;

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
            this.m_stocksDictionary.Add(stockname,new Tuple<string,int>(price,shares));
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


        // get users' information from the disk
        public bool getUserDataFromFile(String FileName)
        {
            bool success = false;
            return success;
        }

        // wirte users information into the disk
        private bool writeUserDataInFile(String FileName, List<Users> data)
        {
            bool success = false;
            return success;
        }

    }
}
