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
        private Dictionary<string, string> m_stocksDictionary = new Dictionary<string, string>();
        
        public Dictionary<string, string> stocksDictionary
        {
            get{
                return m_stocksDictionary;
               }
        }

        // file's name for stock's information stores in the Disk
        private string fileStock;

        // file's name for users' information stores in the Disk
        private string users;

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
        public bool updateAllPrice(List<stockQuote> updateList)
        {
            bool success = false;

            return success;

        }

       
        public void addToTheStockList(string stockName, string price)
        {
            this.m_stocksDictionary.Add(stockname,price);
        }

    }
}
